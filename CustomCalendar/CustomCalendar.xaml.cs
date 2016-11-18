using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Input;
using PropertyChanged;

namespace CustomCalendar
{
    /// <summary>
    ///     Interaction logic for CustomCalendar.xaml
    /// </summary>
    [ImplementPropertyChanged]
    public partial class CustomCalendar
    {
        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register("SelectedDate", typeof(DateTime), typeof(CustomCalendar),
                new FrameworkPropertyMetadata(default(DateTime), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        private DateTime _currentDate;

        public DateTime SelectedDate
        {
            get { return (DateTime)GetValue(SelectedDateProperty); }
            set { SetValue(SelectedDateProperty, value); }
        }

        public double GridHeight { get; set; }

        public DateTime CurrentDate
        {
            get { return _currentDate; }
            set
            {
                _currentDate = value;
                OnPropertyChanged(() => CurrentDate);
            }
        }

        public ObservableCollection<ObservableCollection<CalendarDay>> Days { get; set; }
        public CalendarDay SelectedDay { get; set; }

        public ObservableCollection<CalendarDay> Mondays { get; set; }
        public ObservableCollection<CalendarDay> Tuesdays { get; set; }
        public ObservableCollection<CalendarDay> Wednesdays { get; set; }
        public ObservableCollection<CalendarDay> Thursdays { get; set; }
        public ObservableCollection<CalendarDay> Fridays { get; set; }
        public ObservableCollection<CalendarDay> Saturdays { get; set; }
        public ObservableCollection<CalendarDay> Sundays { get; set; }

        public CustomCalendar()
        {
            InitializeComponent();
            (Content as FrameworkElement).DataContext = this;
          //  DataContext = this;
            grid.SizeChanged += Grid_SizeChanged;
            CurrentDate = DateTime.Now;

            Days = new ObservableCollection<ObservableCollection<CalendarDay>>();
            Mondays = new ObservableCollection<CalendarDay>();
            Tuesdays = new ObservableCollection<CalendarDay>();
            Wednesdays = new ObservableCollection<CalendarDay>();
            Thursdays = new ObservableCollection<CalendarDay>();
            Fridays = new ObservableCollection<CalendarDay>();
            Saturdays = new ObservableCollection<CalendarDay>();
            Sundays = new ObservableCollection<CalendarDay>();

            Days.Add(Mondays);
            Days.Add(Tuesdays);
            Days.Add(Wednesdays);
            Days.Add(Thursdays);
            Days.Add(Fridays);
            Days.Add(Saturdays);
            Days.Add(Sundays);

            for (var i = 0; i < 6; i++)
            {
                Mondays.Add(new CalendarDay());
                Tuesdays.Add(new CalendarDay());
                Wednesdays.Add(new CalendarDay());
                Thursdays.Add(new CalendarDay());
                Fridays.Add(new CalendarDay());
                Saturdays.Add(new CalendarDay());
                Sundays.Add(new CalendarDay());
            }
            Draw();
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridHeight = e.NewSize.Height;
            Draw();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentDate = CurrentDate.AddMonths(-1);
            Draw();
        }

        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentDate = CurrentDate.AddMonths(1);
            Draw();
        }

        private void ButtonBase3_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentDate = DateTime.Now;
            Draw();
        }


        public void Draw()
        {
            var firstOfMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 01);
            var firstDayOfMonth = firstOfMonth.DayOfWeek; // Tuesday in this case
            var mondayDate = DateTime.MinValue;
            switch (firstDayOfMonth)
            {
                case DayOfWeek.Monday:
                    mondayDate = firstOfMonth;
                    break;

                case DayOfWeek.Tuesday:
                    mondayDate = firstOfMonth.AddDays(-1);
                    break;

                case DayOfWeek.Wednesday:
                    mondayDate = firstOfMonth.AddDays(-2);
                    break;

                case DayOfWeek.Thursday:
                    mondayDate = firstOfMonth.AddDays(-3);
                    break;

                case DayOfWeek.Friday:
                    mondayDate = firstOfMonth.AddDays(-4);
                    break;

                case DayOfWeek.Saturday:
                    mondayDate = firstOfMonth.AddDays(-5);
                    break;

                case DayOfWeek.Sunday:
                    mondayDate = firstOfMonth.AddDays(-6);
                    break;
            }

            var tuesdayDate = mondayDate.AddDays(1);
            var wednesdayDate = mondayDate.AddDays(2);
            var thursdayDate = mondayDate.AddDays(3);
            var fridayDate = mondayDate.AddDays(4);
            var saturdayDate = mondayDate.AddDays(5);
            var sundayDate = mondayDate.AddDays(6);

            Mondays.First().Date = mondayDate;
            Tuesdays.First().Date = tuesdayDate;
            Wednesdays.First().Date = wednesdayDate;
            Thursdays.First().Date = thursdayDate;
            Fridays.First().Date = fridayDate;
            Saturdays.First().Date = saturdayDate;
            Sundays.First().Date = sundayDate;

            foreach (var day in Days)
            {
                var d = day.First().Date;
                for (var index = 1; index < day.Count; index++)
                {
                    var i = day[index];
                    i.Date = d.AddDays(7);
                    d = i.Date;
                }

                // Color cells
                foreach (var i in day)
                {
                    i.Draw(CurrentDate);
                    i.Height = GridHeight/6;
                    i.DateSelected += I_DateSelected;
                }
            }
        }

        private void I_DateSelected(object sender, EventArgs e)
        {
            foreach (var day in Days)
            {
                foreach (var d in day)
                {
                    d.IsSelected = false;
                }
            }

            SelectedDate = (sender as CalendarDay).Date;
            (sender as CalendarDay).IsSelected = true;
        }

        private void OnIOnMouseLeftButtonUp(object o, MouseButtonEventArgs args)
        {
            SelectedDate = (o as CalendarDay).Date;
            (o as CalendarDay).IsSelected = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged<T>(Expression<Func<T>> exp)
        {
            //the cast will always succeed
            var memberExpression = (MemberExpression) exp.Body;
            var propertyName = memberExpression.Member.Name;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}