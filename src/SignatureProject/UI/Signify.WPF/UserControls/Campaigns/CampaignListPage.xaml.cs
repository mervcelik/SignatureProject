using ApiService.Services;
using Domain.Entities;
using Signify.WPF.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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

namespace Signify.WPF.UserControls.Campaigns;

/// <summary>
/// Interaction logic for CampaignListPage.xaml
/// </summary>
public partial class CampaignListPage : UserControl
{
    private CampaignViewModel _viewModel;
    public CampaignListPage()
    {
        InitializeComponent();


  
     
    }

    private async void Next_Click(object sender, RoutedEventArgs e) => await _viewModel.NextPage();
    private async void Previous_Click(object sender, RoutedEventArgs e) => await _viewModel.PreviousPage();

    private async void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        _viewModel = new CampaignViewModel();
        await _viewModel.InitData();
        DataContext = _viewModel;
    }
}



public class CampaignViewModel
{
    private int _currentPage = 0;
    private int _pageSize = 3;
    private int _maxPage = 0;
    public ObservableCollection<CampaignModel> Campaigns { get; set; }
    CampaignApiService _campaignApiService;

    public async Task InitData()
    {
        _campaignApiService = App.GetService<CampaignApiService>();
        await LoadPage();
    }
    public async Task LoadPage()
    {
        var result = RNH.GetOrThrow(await _campaignApiService.GetList(new Core.Application.Request.PageRequest { PageIndex = _currentPage, PageSize = _pageSize }));
        _maxPage = result.Pages;
        Campaigns =new ObservableCollection<CampaignModel>(result.Items.Select(c => new CampaignModel
        {
            Name = c.Name,
            Description = c.Description,
            ExpiredDate = c.ExpiredDate,
            MaxParticipants = c.Quantity,
            CurrentParticipants = 20
        }));
    }

    public async Task NextPage()
    {
        if (_currentPage < _maxPage)
        {
            _currentPage++;
            await LoadPage();
        }
    }

    public async Task PreviousPage()
    {
        if (_currentPage > 0)
        {
            _currentPage--;
            await LoadPage();
        }
    }
}

public class CampaignModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime ExpiredDate { get; set; }
    public int MaxParticipants { get; set; }
    public int CurrentParticipants { get; set; }

    public double ParticipationRate =>
        MaxParticipants > 0 ? (double)CurrentParticipants / MaxParticipants * 100 : 0;
}