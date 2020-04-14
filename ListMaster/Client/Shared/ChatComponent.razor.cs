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
    public partial class ChatComponent : ComponentBase
    {
        [CascadingParameter] Task<AuthenticationState> authenticationStateTask { get; set; }

        private HubConnection _hubConnection;
        private List<ChatMessageViewModel> _messages = new List<ChatMessageViewModel>();
        private string _messageInput;

        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/chatHub"))
                .Build();

            _hubConnection.On<ChatMessageViewModel>("ReceiveMessage", (message) =>
            {
                var encodedMsg = message;
                _messages.Add(encodedMsg);
                StateHasChanged();
            });

            _hubConnection.On<List<ChatMessageViewModel>>("ReceiveCurrentMessages", (messages) =>
            {
                _messages = messages;
                StateHasChanged();
            });
            var authState = await authenticationStateTask;

            await _hubConnection.StartAsync().ContinueWith(delegate { LoadCurrentMessages(); });
        }

        Task LoadCurrentMessages() =>
            _hubConnection.SendAsync("GetCurrentMessages", _hubConnection.ConnectionId);

        async Task Send()
        {
            var authState = await authenticationStateTask;
            var user = authState.User;

            await _hubConnection.SendAsync("SendMessage", new ChatMessageViewModel()
            {
                MessageBody = _messageInput,
                Username = user.Identity.Name,
                Kudos = 0,
                CreatedDate = DateTime.Now

            }).ContinueWith(delegate { ClearChatTextBox(); });

        }
            

        Task ClearChatTextBox()
        {
            _messageInput = String.Empty;
            return Task.CompletedTask;
        }

        public bool IsConnected =>
            _hubConnection.State == HubConnectionState.Connected;

    }


}
