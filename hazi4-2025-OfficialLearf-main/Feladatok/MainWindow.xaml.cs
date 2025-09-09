using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MultiThreadedApp.AppLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;

namespace MultiThreadedApp
{
    public sealed partial class MainWindow : Window
    {
        private Game game = new Game();
        private List<TextBlock> bikeTextBlocks = new List<TextBlock>();
        private Windows.System.DispatcherQueue dispatcher;

        public MainWindow()
        {
            this.InitializeComponent();
            dispatcher = Windows.System.DispatcherQueue.GetForCurrentThread();
            startLineRect.Margin = new Thickness(Game.StartLinePosition, 15, 0, 10);
            depoPanel.Margin = new Thickness(Game.DepoPosition - 47, 15, 0, 0);
            finishLineRect.Margin = new Thickness(Game.FinishLinePosition, 15, 0, 10);
            AddKeyboardAcceleratorToChangeTheme();
        }

        private void AddKeyboardAcceleratorToChangeTheme()
        {
            rootPanel.RequestedTheme = Application.Current.RequestedTheme == ApplicationTheme.Light ? ElementTheme.Light : ElementTheme.Dark;
            var accelerator = new KeyboardAccelerator()
            {
                Modifiers = VirtualKeyModifiers.Control,
                Key = VirtualKey.T,
            };
            accelerator.Invoked += (UIElement, args) =>
            {
                rootPanel.RequestedTheme = rootPanel.RequestedTheme == ElementTheme.Light
                    ? rootPanel.RequestedTheme = ElementTheme.Dark
                    : ElementTheme.Light;
                args.Handled = true;
            };
            rootPanel.KeyboardAcceleratorPlacementMode = KeyboardAcceleratorPlacementMode.Hidden;
            rootPanel.KeyboardAccelerators.Add(accelerator);
        }

        private void PrepareRaceButton_Click(object sender, RoutedEventArgs e)
        {
            prepareRaceButton.IsEnabled = false;
            game.PrepareRace(UpdateBikeUI);

            foreach (var bike in game.Bikes)
            {
                var bikeTextBlock = new TextBlock()
                {
                    Text = "b",
                    FontFamily = new FontFamily("Webdings"),
                    FontSize = 64,
                    Margin = new Thickness(10, 0, 0, 0)
                };

                bikesPanel.Children.Add(bikeTextBlock);
                bikeTextBlocks.Add(bikeTextBlock);
            }
        }

        private void StartRaceButton_Click(object sender, RoutedEventArgs e)
        {
            startRaceButton.IsEnabled = false;
            game.StartBikes();
        }

        private void StartNextFromDepoButton_Click(object sender, RoutedEventArgs e)
        {
            game.StartNextBikeFromDepo();
        }

        private void UpdateBikeUI(Bike bike)
        {
            // Előfordulhat, hogy az UpdateBikeUI olyan korán hívódik, hogy a
            // bikeTextBlocks még nincs feltöltve, ilyenkor még nem tudjuk frissíteni
            // a felületet, térjünk vissza.
            if (bikeTextBlocks.Count != game.Bikes.Count)
                return;


            int marginAdjustmentForWheel = 8;

               // Biciklihez tartozó TextBlock kikeresése (azonos tömbindex alapján).
            var tbBike = bikeTextBlocks[game.Bikes.IndexOf(bike)];

            DispatcherQueue.TryEnqueue(() => {
                // Akkor még ne állítsuk a bicikli pozícióját, amikor a mérete a layout során nem
                // került meghatározásra (különben ugrálna a bicikli, hiszen alább, a margó beállításakor
                // "érvénytelen" 0 szélességértékkel számolnánk.
                if (tbBike.ActualWidth == 0)
                    return;

                // Az ablak 0,0 pontja az origó, ehhez képest nézzük a start/depó/finish vonalat.
                // A gomb jobb szélén van a kerék, de ezt a gomb bal oldalára kell mozgatni: ActualWidth-et ki kell vonni.
                tbBike.Margin = new Thickness(bike.Position - tbBike.ActualWidth + marginAdjustmentForWheel, 0, 0, 0);

                if (bike.IsWinner)
                    tbBike.Text = "%"; // display a cup
            } ); 
        }

    }
}