using ListMaster.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListMaster.Client.Shared
{
    public partial class MasterListComponent : ComponentBase
    {
        [CascadingParameter] Task<AuthenticationState> authenticationStateTask { get; set; }

        private HubConnection _hubConnection;
        private List<ListoidViewModel> _listitems = new List<ListoidViewModel>();
        private string _listTitle;

        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/chatHub"))
                .Build();

            _hubConnection.On<string, List<ListoidViewModel>>("ReceiveCurrentMasterList", (masterlistname, listitems) =>
            {
                _listTitle = masterlistname;                
                _listitems = listitems;
                StateHasChanged();
            });

            var authState = await authenticationStateTask;

            await _hubConnection.StartAsync().ContinueWith(delegate { LoadCurrentMasterList(); });

        }

        Task LoadCurrentMasterList() =>
            _hubConnection.SendAsync("GetCurrentMasterList", _hubConnection.ConnectionId);

        async Task GiveAKudo(int id)
        {
            var authState = await authenticationStateTask;
            var user = authState.User;

            await _hubConnection.SendAsync("SendAKudo", _hubConnection.ConnectionId, new KudoViewModel()
            {
                ListoidId = id,
                username = user.Identity.Name,
            });
        }
    }
}
