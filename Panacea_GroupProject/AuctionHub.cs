using BusinessObjects;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;

namespace Panacea_GroupProject
{
	public class AuctionHub:Hub
	{
		private static readonly Dictionary<int, HashSet<string>> ConnectedUsers = new Dictionary<int, HashSet<string>>();

		public override async Task OnConnectedAsync()
		{
			var httpContext = Context.GetHttpContext();
			var auctionId = httpContext.Request.Query["auctionId"].ToString(); // Lấy auctionId từ query string

			if (!string.IsNullOrEmpty(auctionId))
			{
				await Groups.AddToGroupAsync(Context.ConnectionId, auctionId);
			}

			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			var httpContext = Context.GetHttpContext();
			var auctionId = httpContext.Request.Query["auctionId"].ToString(); // Lấy auctionId từ query string

			if (!string.IsNullOrEmpty(auctionId))
			{
				await Groups.RemoveFromGroupAsync(Context.ConnectionId, auctionId);
			}

			await base.OnDisconnectedAsync(exception);
		}

		public async Task SendMessageChat(string user, string message, string auctionId)
		{
			try
			{
				if (string.IsNullOrEmpty(auctionId))
				{
					throw new ArgumentException("Auction ID cannot be null or empty", nameof(auctionId));
				}

				// Gửi tin nhắn đến group auction cụ thể
				await Clients.Group(auctionId).SendAsync("ReceiveMessageChat", user, message);
			}
			catch (Exception ex)
			{
				// Ghi log cho exception (có thể sử dụng logging framework)
				Console.WriteLine($"Error in SendMessageChat: {ex.Message}");
				throw;
			}
		}
		public async Task SendBidUpdate(int auctionId)
		{
			await Clients.All.SendAsync("ReceiveNewBid", auctionId);
		}

		 

	}
}
