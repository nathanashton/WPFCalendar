using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using PropertyChanged;

namespace CustomCalendar
{
    /// <summary>
    ///     Interaction logic for CalendarDay.xaml
    /// </summary>
    [ImplementPropertyChanged]
    public partial class CalendarDay
    {
        public static readonly DependencyProperty IsSelectedProperty =
    DependencyProperty.Register("IsSelected", typeof(bool), typeof(CalendarDay),
        new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        
        public bool IsSelected
        {
            get { return (bool) GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        
        public static readonly DependencyProperty DateProperty = DependencyProperty.Register("Date", typeof(DateTime),
            typeof(CalendarDay));

        public static readonly DependencyProperty IsTodayProperty = DependencyProperty.Register("IsToday", typeof(bool),
            typeof(CalendarDay));

        public static readonly DependencyProperty IsDifferentMonthProperty =
            DependencyProperty.Register("IsDifferentMonth", typeof(bool), typeof(CalendarDay));

        public ObservableCollection<string> Transactions { get; set; }

        public DateTime Date
        {
            get { return (DateTime) GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public bool IsToday
        {
            get { return (bool) GetValue(IsTodayProperty); }
            set { SetValue(IsTodayProperty, value); }
        }





        public event EventHandler DateSelected;

        private void OnDateSelected()
        {
            if (DateSelected != null)
            {
                DateSelected(this, null);
            }
        }



        public bool IsDifferentMonth
        {
            get { return (bool) GetValue(IsDifferentMonthProperty); }
            set { SetValue(IsDifferentMonthProperty, value); }
        }

        public CalendarDay()
        {
            InitializeComponent();
            Transactions = new ObservableCollection<string>();
            DataContext = this;
        }

        public void Draw(DateTime currentDate)
        {
            var now = DateTime.Now;
            IsToday = false;
            IsDifferentMonth = false;
            if ((now.Year == Date.Year) && (now.Month == Date.Month) && (now.Day == Date.Day))
                IsToday = true;
            if ((currentDate.Month != Date.Month) && (currentDate.Year != Date.Month))
                IsDifferentMonth = true;
        }

        private void UIElement_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           OnDateSelected();
        }
    }
}