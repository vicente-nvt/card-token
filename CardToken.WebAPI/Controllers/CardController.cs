using CardToken.Application;
using CardToken.WebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CardToken.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private CardCreation _cardCreation;

        public CardController(CardCreation cardCreation)
        {
            _cardCreation = cardCreation;
        }

        [HttpPost, Route("registerCard")]
        public ActionResult<CardDTO> RegisterCard([FromBody] CardInfoDTO cardInfo)
        {
            var cardDTO = _cardCreation.CreateNewCard(cardInfo.CardNumber, cardInfo.CVV);

            return Ok(cardDTO);
        }
    }
}
