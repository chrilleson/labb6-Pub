using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Lab6_Pub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Thread Bouncer = new Thread(() => OpenLength(10));

            new Thread(() =>
            {
                //Sätt tid för hur länge baren ska vara öppet.
                OpenLength(10);

                Environment.Exit(0);
            }).Start();

            //Bouncer.Start();
        }
        static void OpenLength(int maxNum)
        {
            for (int i = 0; i <= maxNum; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
        }
    }
}
