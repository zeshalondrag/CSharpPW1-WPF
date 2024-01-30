using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Button[] buttons;
        bool user = true;

        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button button in buttons)
            {
                button.IsEnabled = true;
                button.Content = "";
            }

            start.Content = "Reset";

            if (!user)
            {
                chatgpt(user);
            }

            result.Content = "";
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void allbutton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (string.IsNullOrEmpty(clickedButton.Content as string))
            {
                if (user)
                {
                    clickedButton.Content = "X";
                }
                else
                {
                    clickedButton.Content = "O";
                }

                if (IsVictory(buttons) || IsTie(buttons))
                {
                    if (IsVictory(buttons))
                    {
                        VictoryMessage(user);
                    }
                    else
                    {
                        TieMessage();
                    }
                    reset();
                }
                else
                {
                    chatgpt(user);

                    if (IsVictory(buttons) || IsTie(buttons))
                    {
                        if (IsVictory(buttons))
                        {
                            VictoryMessage(user);
                        }
                        else
                        {
                            TieMessage();
                        }
                        reset();
                    }
                }
            }
        }

        Random random = new Random();

        private void chatgpt(bool ai)
        {
            Button[] buttons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
            int box = 0;

            for (int attempt = 0; attempt < 10; attempt++)
            {
                int choice = random.Next(buttons.Length);

                if (string.IsNullOrEmpty(buttons[choice].Content as string))
                {
                    buttons[choice].Content = ai ? "O" : "X";
                    break;
                }

                box++;
            }
        }


        private bool IsRowVictory(Button[] buttons, int startIndex, int step)
        {
            return buttons[startIndex].Content == buttons[startIndex + step].Content &&
                   buttons[startIndex + step].Content == buttons[startIndex + 2 * step].Content &&
                   !string.IsNullOrEmpty((string)buttons[startIndex].Content);
        }

        private bool IsColumnVictory(Button[] buttons, int startIndex, int step)
        {
            return buttons[startIndex].Content == buttons[startIndex + 3 * step].Content &&
                   buttons[startIndex + 3 * step].Content == buttons[startIndex + 6 * step].Content &&
                   !string.IsNullOrEmpty((string)buttons[startIndex].Content);
        }

        private bool IsDiagonalVictory(Button[] buttons, int startIndex, int step)
        {
            return buttons[startIndex].Content == buttons[startIndex + step].Content &&
                   buttons[startIndex + step].Content == buttons[startIndex + 2 * step].Content &&
                   !string.IsNullOrEmpty((string)buttons[startIndex].Content);
        }

        private bool IsVictory(Button[] buttons)
        {
            for (int i = 0; i < 3; i++)
            {
                if (IsRowVictory(buttons, i * 3, 1) || IsColumnVictory(buttons, i, 1))
                {
                    return true;
                }
            }

            return IsDiagonalVictory(buttons, 0, 4) || IsDiagonalVictory(buttons, 2, 2);
        }

        private bool IsTie(Button[] buttons)
        {
            return buttons.All(button => !string.IsNullOrEmpty((string)button.Content));
        }

        private void VictoryMessage(bool user)
        {
            result.Content = user ? "Вы выиграли!" : "Бот выиграл!";
        }

        private void TieMessage()
        {
            result.Content = "Ничья!";
        }

        private void reset()
        {
            foreach (Button button in buttons)
            {
                button.IsEnabled = false;
            }

            start.Content = "Start";
            user = !user;
        }
    }
}
