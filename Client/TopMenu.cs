using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Client
{
    public class TopMenu
    {
        public static ContextMenu GetTopMenu(string user,string header, HelpString helpString)
        {
            ContextMenu topMenu = new ContextMenu();
            MenuItem connect = new MenuItem
            {
                Header = "Connect"
            };

            //Action Sub Menu
            MenuItem actionMenu = new MenuItem
            {
                Header = "Action"
            };
            //Items of Sub Menu
            MenuItem sendMsg = new MenuItem
            {
                Header = "Send Message"
            };
            MenuItem play = new MenuItem
            {
                Header = "Play Backgammon"
            };

            actionMenu.Items.Add(sendMsg);
            actionMenu.Items.Add(play);

            //Help Sub Menu
            MenuItem help = new MenuItem
            {
                Header = "Help"
            };
            //Items of Sub Menu
            MenuItem howToPlay = new MenuItem
            {
                Header = header
            };
            TextBlock howToPlayText = new TextBlock
            {
                //Text = GetHowToPlay(),
                Width = 200,
                Background = new SolidColorBrush(Colors.LightGray),
                Padding = new Thickness(5),
                TextWrapping = TextWrapping.Wrap
            };
            switch (helpString)
            {
                case HelpString.HelpForLogin:
                    howToPlayText.Text = GetHelpForLogin();
                    break;
                case HelpString.HelpForChat:
                    howToPlayText.Text = GetHelpForChat();
                    break;
                case HelpString.HelpForRegistration:
                    howToPlayText.Text = GetHelpForRegistration();
                    break;
                case HelpString.HelpForPrChat:
                    howToPlayText.Text = GetHelpForPrChat();
                    break;
                default:
                    break;
            }
            howToPlay.Items.Add(howToPlayText);

            MenuItem about = new MenuItem
            {
                Header = "About"
            };
            TextBlock aboutText = new TextBlock
            {
                Text = GetAbout(),
                Width = 200,
                Background = new SolidColorBrush(Colors.LightGray),
                Padding = new Thickness(5),
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Justify
            };
            about.Items.Add(aboutText);

            help.Items.Add(howToPlay);
            help.Items.Add(about);

            topMenu.Items.Add(connect);
            topMenu.Items.Add(actionMenu);
            topMenu.Items.Add(help);

            return topMenu;
        }

        public static string GetHelpForLogin()
        {
            string text = "Enter your user name and password\n" +
                          "for enter to aplication or press\n" +
                          "'Registration' for enter to\n" +
                          "registration screen\n" +
                          "Ex:\n" +
                          "afek 123\n" +
                          "niv 123\n" +
                          "david 123\n";
            return text;
        }
        public static string GetHelpForChat()
        {
            string text = "You can chat with all users\n" +
                          "thet online.Select a user\n" +
                          "and press 'Chat' for\n" +
                          "starting private chat or\n" +
                          "'Game' for game";
            return text;
        }
        public static string GetHelpForRegistration()
        {
            string text = "In this screen you can\n" +
                          "to register.\n" +
                          "Select user and password\n" +
                          "and enter your personal\n" +
                          "data";
            return text;
        }
        public static string GetHelpForPrChat()
        {
            string text = "In this screen you chat\n" +
                          "with selected user and\n" +
                          "start to game with his by\n" +
                          "pressing 'Back' you can to\n" +
                          "return to previous screen\n" +
                          "(general chat)";
            return text;
        }
        public static string GetHelpForGame()
        {
            string text = " In this screen you play with selected user.\n" +
                          "The goal of the game is to bring all the\n" +
                          "checkers out of the board.\n" +
                          " By pressing 'Roll' you can rollint the dices\n" +
                          "or skip the roll by pressing 'Resigh'\n" +
                          "in case that moving is imposible.\n" +
                          " You can move your checkers\n" +
                          "according to the fallen numbers.\n" +
                          " One checker for the sum of the dropped\n" +
                          "numbers or two respectively.\n" +
                          " You can knock out the opponent's\n" +
                          "checkerboard by standing on it\n" +
                          " You can output your checkers from the board\n" +
                          "only if the bar does not content your checkers";
            return text;
        }
        public static string GetAbout()
        {
            string text = " The program is written within\n " +
                          "the framework of the course .Net\n" +
                          "is based on the WCF,WPF,\n" +
                          "Entity Framework architecture";
            return text;
        }
    }
    public enum HelpString
    {
        HelpForLogin = 1,
        HelpForChat,
        HelpForRegistration,
        HelpForPrChat,
        HelpForGame
    }

}
