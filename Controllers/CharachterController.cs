using dotnet_rpg.DTO.Character;
using dotnet_rpg.Service.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharachterController : ControllerBase
    {
        private readonly ICharacterService characterService;

        public CharachterController(ICharacterService characterService)
        {
            this.characterService = characterService;
        }

        
        [HttpGet("getAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> Get(){
            return Ok(await characterService.GetAllCharcters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> GetSingle(int id){
            return Ok(await characterService.GetCharacterById(id));
        }
        [HttpPost("character")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> AddCharacter(AddCharacterDTO character){
            return Ok(await characterService.AddCharacter(character));
        }

        [HttpPut("character")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> UpdateCharacter(UdpateCharacterDTO character){
            ServiceResponse<GetCharacterDTO> response = await characterService.UpdateCharacter(character);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> DeleteChracter(int id){
            ServiceResponse<List<GetCharacterDTO>> serviceResponse = await characterService.DeleteCharacter(id);
            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }

            return Ok(serviceResponse);
        }
    }
}