// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/IPlayerRound.cs


using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.Player;
using System.Reactive.Subjects;
using System.Threading.Tasks;


/// <summary>	
/// 	This Interface handles an instance of a players turn in black jack 
/// 	
///		Subject<BlackJackActions.PlayerActions> _playerActionSubject										: Regesters player actions events
///		Task									PlayerTurn(ICardDeck cardDeck, IPlayer player)				: This method is called when the player is taking their turn and register their actions. 
///																												It handles both the primary and split hand.
///	</summary>

namespace BlackJackV2.Models.GameLogic
{
	public interface IPlayerRound <TImage, TValue>
	{
		public Subject<BlackJackActions.PlayerActions> _playerActionSubject { get; }
		public Task PlayerTurn(ICardDeck<TImage, TValue> cardDeck, IPlayer<TImage, TValue> player);
	}
}
