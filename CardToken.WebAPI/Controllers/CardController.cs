using Microsoft.AspNetCore.Authorization;
using CardToken.Application.CardCreation;
using CardToken.Application.CardValidation;
using CardToken.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using ApplicationException = CardToken.Application.ApplicationException;

namespace CardToken.WebAPI.DTOs
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardController : ControllerBase
    {
        private CardCreation _cardCreation;
        private CardValidation _cardValidation;

        public CardController(CardCreation cardCreation, CardValidation cardValidation)
        {
            _cardCreation = cardCreation;
            _cardValidation = cardValidation;
        }

        /// <summary>
        /// Allows clients to register new user cards.
        /// </summary>
        /// <param name="cardInfo">An object with necessary info to register a new card</param>
        /// <returns>An object containing the result of card registration</returns>
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(CardDTO), 200)]
        [HttpPost, Route("register")]
        public ActionResult<CardDTO> RegisterCard([FromBody] CardInfoDTO cardInfo)
        {
            CardDTO cardDto;
            try
            {
                cardDto = _cardCreation.CreateNewCard(cardInfo.CardNumber, cardInfo.CVV);
                return Ok(cardDto);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Allows clients to validate its card.
        /// </summary>
        /// <param name="dataToValidateDto">An object with necessary info to validate an existing card</param>
        /// <returns>An object containing the result of card validation, true of false.</returns>
        [HttpPost, Route("validate")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(ValidationDTO), 200)]
        public ActionResult<ValidationDTO> ValidateCard([FromBody] DataToValidateDTO dataToValidateDto)
        {
            ValidationDTO validationDto;
            try
            {
                validationDto = _cardValidation.Validate(dataToValidateDto.Token, dataToValidateDto.Cvv, dataToValidateDto.RegistrationDate);
                return Ok(validationDto);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
