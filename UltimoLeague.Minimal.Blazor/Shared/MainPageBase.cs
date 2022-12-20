using Microsoft.AspNetCore.Components;
using MudBlazor;
using UltimoLeague.Minimal.Blazor.Interfaces;
using UltimoLeague.Minimal.Blazor.Pages.Dialogs;
using UltimoLeague.Minimal.Contracts.Dtos;

namespace UltimoLeague.Minimal.Blazor.Shared;

public class MainPageBase : LayoutComponentBase
{
    [Inject] public ISessionService _sessionService { get; set; }
    [Inject] public IDialogService _dialogService { get; set; }
    public SportMinimalDto? currentSport;

    protected override async Task OnInitializedAsync()
    {
        currentSport = await _sessionService.GetCurrentSport();

        while (currentSport is null)
        {
            await HandleCurrentSport();
        }
    }

    public async Task HandleCurrentSport()
    {
        var result = await _dialogService.Show<SelectSport>("Select Sport").Result;
        if (!result.Cancelled)
        {
            currentSport = result.Data as SportMinimalDto;
            this.StateHasChanged();
        }
    }
}
