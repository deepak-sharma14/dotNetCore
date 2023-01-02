using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.DTO.Character;

namespace dotnet_rpg.Service.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharcters();

        Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id);

        Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO character);

        Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UdpateCharacterDTO character);

        Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int Id);
    }
}