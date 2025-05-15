using System.ComponentModel;
using System.Runtime.CompilerServices;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Microsoft.EntityFrameworkCore;
using FoxVill.DataBase;

namespace FoxVill.MainServices.ChartService;

public class ChartService : INotifyPropertyChanged
{
    private readonly DatabaseContext _dbContext;
    private readonly Lazy<PlotModel> _model = new(() => new PlotModel());

    public event PropertyChangedEventHandler? PropertyChanged;

    public PlotModel Model => _model.Value;

    public ChartService(DatabaseContext context)
    {
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        SetPlotSettings();
    }

    public void SetPlotSettings()
    {
        _dbContext.Products.Load();
        _dbContext.OrderItems.Load();
        _dbContext.Orders.Load();

        var productsByDate = _dbContext.OrderItems
            .GroupBy(u => u.Order.OrderDate)
            .Select(g => new
            {
                Date = g.Key,
                Count = g.Count(),
            })
            .OrderBy(x => x.Date)
            .ToList();

        if (!productsByDate.Any())
        {
            ClearPlot();
            return;
        }

        ConfigureAxes();
        AddSeries(productsByDate);
        OnPropertyChanged(nameof(Model));
    }

    private void ClearPlot()
    {
        Model.Axes.Clear();
        Model.Series.Clear();
        OnPropertyChanged(nameof(Model)); // Обновляем после очистки
    }

    private void ConfigureAxes()
    {
        Model.Axes.Add(new DateTimeAxis
        {
            Position = AxisPosition.Bottom,
            Title = "Дата",
            MajorGridlineStyle = LineStyle.Solid,
            MinorGridlineStyle = LineStyle.Dot,
            IntervalType = DateTimeIntervalType.Days,
            MajorStep = 365
        });

        Model.Axes.Add(new LinearAxis
        {
            Position = AxisPosition.Left,
            Title = "Покупки",
            MajorGridlineStyle = LineStyle.Solid,
            MinorGridlineStyle = LineStyle.Dot,
            StringFormat = "0",
            MajorStep = 1
        });
    }

    private void AddSeries(IEnumerable<dynamic> productsByDate)
    {
        var series = new LineSeries
        {
            Title = "Покупки",
            MarkerType = MarkerType.Circle
        };

        foreach (var item in productsByDate)
        {
            series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(item.Date), item.Count));
        }

        Model.Series.Add(series);
    }

    private void OnPropertyChanged([CallerMemberName] string property = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}
