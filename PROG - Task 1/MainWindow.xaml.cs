using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace PROG___Task_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Variables declared for timer
        DispatcherTimer _timer;
        TimeSpan _time;

        public MainWindow()
        {
            InitializeComponent();
            //Used to block off 'Check' Button until they have inputted values into the second block
            btnCheck.IsEnabled = false;
        }

        //Lists are declared here 
        //Author surname and call numbers will be recorded into this list
        List<Books> bkList = new List<Books>();
        List<Books> bkUserList = new List<Books>();

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //Used to clear all the items to make place for new ones
            bkList.Clear();
            bkUserList.Clear();
            lstSorted.Items.Clear();
            lstUnsorted.Items.Clear();

            //This is where the timer will start
            Timer();

            //Random number generator
            Random rnd = new Random();

            //Authors first 3 letters of their surnames
            //Recorded into an array
            string[] arAuthors = new string[] { "NOR", "RUS", "RIC",
                                                "PER", "MSC", "HAM",
                                                "RAI", "MAZ", "VER"};

            for(int i = 1; i <= 10; i++)
            {
                //Random numbers and authors names are given then recorded into the list
                bkList.Add(new Books(rnd.Next(1, 12), arAuthors[rnd.Next(0, arAuthors.Length - 1)]));
            }

            foreach(Books x in bkList)
            {
                //This is used to display call numbers and author surname combinations
                lstUnsorted.Items.Add(string.Join(" ", x.CallNum, x.Author));
            }
            
            bubbleSort(bkList);
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < lstSorted.Items.Count; i++)
                {
                    //This is used to determine the index of each call number and author combination
                    string item = lstSorted.Items[i].ToString();
                    int spaceIndex = item.IndexOf(" ");
                    int callNum = Convert.ToInt32(item.Substring(0, spaceIndex));
                    string Auth = item.Substring(spaceIndex + 1);

                    //This adds the values to the class
                    bkUserList.Add(new Books(callNum, Auth));
                }

                if (bkList.SequenceEqual(bkUserList, new Books()))
                {
                    //This method was taken from StackOverflow
                    //https://stackoverflow.com/questions/8678862/increment-by-1-in-c-sharp
                    //Author is Eugen Rieck
                    //Accessed 20 September 2022
                    //Used to keep Track a HighScore
                    int x = 0;
                    x = x + 1;

                    //This method was taken from StackOverflow
                    //https://stackoverflow.com/questions/63547755/how-can-i-integrate-toast-notifications-in-wpf-app
                    //Author is Thomas Shephard
                    //Accessed 19 September 2022
                    //Used to display if answer is correct in a neat and fancy way
                    notifier.ShowSuccess("CORRECT");

                    //This method was taken from StackOverflow
                    //https://stackoverflow.com/questions/4902464/how-to-add-text-to-a-wpf-label-in-code
                    //Author is Matas Vaitkevicius
                    //Accessed 20 September 2022
                    //Used to display score
                    lblScore.Content = x.ToString();

                }
                else
                {
                    //This method was taken from StackOverflow
                    //https://stackoverflow.com/questions/63547755/how-can-i-integrate-toast-notifications-in-wpf-app
                    //Author is Thomas Shephard
                    //Accessed 19 September 2022
                    //Used to display if answer is incorrect in a neat and fancy way
                    notifier.ShowError("INCORRECT");
                }
            }
            catch
            {
                MessageBox.Show("An Error Occured");
            }
        }

        //This method was taken from Youtube
        //https://www.youtube.com/watch?v=THV5BW5WW_o
        //Author is SingletonSean
        //Accessed 19 September 2022
        //Used to drag and drop one item from one text block to another
        private void lstUnsorted_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                base.OnMouseMove(e);

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    DragDrop.DoDragDrop(this, lstUnsorted.SelectedItem.ToString(), DragDropEffects.Move);
                }

                pgbScore.Visibility = Visibility.Visible;
                pgbScore.Value = lstSorted.Items.Count * 10;
            }
            catch
            {
                MessageBox.Show("An Error Occured");
            }
        }

        //This method was taken from Youtube
        //https://www.youtube.com/watch?v=THV5BW5WW_o
        //Author is SingletonSean
        //Accessed 19 September 2022
        //Used to drag and drop one item from one text block to another
        private void lstSorted_Drop(object sender, DragEventArgs e)
        {
            try
            {
                var myObj = e.Data.GetData(DataFormats.Text);

                lstUnsorted.Items.Remove(lstUnsorted.SelectedItem);
                lstSorted.Items.Add(myObj);

                if (lstSorted.Items.Contains(lstSorted.SelectedItem))
                {
                    lstSorted.Items.Remove(lstUnsorted.SelectedItem);
                    lstSorted.Items.Remove(lstSorted.SelectedItem);
                }

                if (lstSorted.Items.Count > 0)
                {
                    btnCheck.IsEnabled = true;
                }
            }
            catch
            {
                MessageBox.Show("An Error Occured");
            }
        }

        //This method was taken from Youtube
        //https://www.youtube.com/watch?v=THV5BW5WW_o
        //Author is SingletonSean
        //Accessed 19 September 2022
        //Used to drag and drop one item from one text block to another
        private void lstSorted_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                base.OnMouseMove(e);

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    DragDrop.DoDragDrop(this, lstSorted.SelectedItem.ToString(), DragDropEffects.Move);
                }
            }
            catch
            {
                MessageBox.Show("An Error Occured");
            }
        }

        //This method was taken from Youtube
        //https://www.youtube.com/watch?v=THV5BW5WW_o
        //Author is SingletonSean
        //Accessed 19 September 2022
        //Used to drag and drop one item from one text block to another
        private void lstUnsorted_Drop(object sender, DragEventArgs e)
        {
            try
            {
                var myObj = e.Data.GetData(DataFormats.Text);

                lstSorted.Items.Remove(lstSorted.SelectedItem);
                lstUnsorted.Items.Add(myObj);

                if (lstUnsorted.Items.Contains(lstUnsorted.SelectedItem))
                {
                    lstUnsorted.Items.Remove(lstSorted.SelectedItem);
                    lstUnsorted.Items.Remove(lstUnsorted.SelectedItem);
                }

                if (lstSorted.Items.Count < 1)
                {
                    btnCheck.IsEnabled = false;
                }
            }
            catch
            {
                MessageBox.Show("An Error Occured");
            }
        }

        //Parts of this method was taken from Kabelo Tlhape
        //Accessed 19 September 2022
        //Used to display toast notification
        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        //This Method was taken from TutorialsPoint
        //https://www.tutorialspoint.com/Bubble-Sort-program-in-Chash
        //Accessed 20 September 2022
        //Used to sort callnumbers and authors
        //Once sorted it is then recorded into a new list
        private void bubbleSort(List<Books> bkList)
        {
            try
            {
                for (int i = 0; i < bkList.Count - 1; i++)
                {
                    for (int k = (i + 1); k < bkList.Count; k++)
                    {
                        if (bkList[i].CompareTo(bkList[k]) == 1)
                        {
                            Books temp = bkList[i];
                            bkList[i] = bkList[k];
                            bkList[k] = temp;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("An Error Occured");
            }
        }
        
        //This Method was taken from stack overflow
        //https://stackoverflow.com/questions/16748371/how-to-make-a-wpf-countdown-timer
        //Author is kmatyaszek
        //Accessed 20 September 2022
        //Used to display a timer
        private void Timer()
        {
            try
            {
                _time = TimeSpan.FromSeconds(120);

                _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                {
                    tbTime.Text = _time.ToString("c");
                    if (_time == TimeSpan.Zero) _timer.Stop();
                    _time = _time.Add(TimeSpan.FromSeconds(-1));
                }, Application.Current.Dispatcher);

                _timer.Start();
            }
            catch
            {
                MessageBox.Show("An Error Occured");
            }
        }

    }
}
