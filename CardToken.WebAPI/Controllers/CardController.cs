using CardToken.Application.CardCreation;
using CardToken.Application.CardValidation;
using CardToken.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using ApplicationException = CardToken.Application.ApplicationException;

namespace CardToken.WebAPI.DTOs
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private CardCreation _cardCreation;
        private CardValidation _cardValidation;

        public CardController(CardCreation cardCreation, CardValidation cardValidation)
        {
            _cardCreation = cardCreation;
            _cardValidation = cardValidation;
        }

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

        [HttpPost, Route("validate")]
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
