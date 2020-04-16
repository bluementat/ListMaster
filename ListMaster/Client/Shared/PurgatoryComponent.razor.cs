﻿using ListMaster.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListMaster.Client.Shared
{
    public partial class PurgatoryComponent : ComponentBase
    {
        [CascadingParameter] Task<AuthenticationState> authenticationStateTask { get; set; }

        private HubConnection _hubConnection;
        private List<ListoidViewModel> _listitems = new List<ListoidViewModel>();

        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/chatHub"))
                .Build();

            _hubConnection.On<List<ListoidViewModel>>("ReceiveCurrentPurgatoryItems", (listitems) =>
            {
                _listitems = listitems;
                StateHasChanged();
            });

            var authState = await authenticationStateTask;

            await _hubConnection.StartAsync().ContinueWith(delegate { LoadCurrentPurgatoryItems(); });
        }

        Task LoadCurrentPurgatoryItems() =>
            _hubConnection.SendAsync("GetCurrentPurgatoryItems", _hubConnection.ConnectionId);

    }
}
