using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.DTO.Character;

namespace dotnet_rpg.Service.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character {Id = 1, Name = "Sam"}
        };
        private readonly IMapper mapper;

        public CharacterService(IMapper mapper)
        {
            this.mapper = mapper;
        }
        
        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO character)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            Character item = mapper.Map<Character>(character);
            item.Id = characters.Max(c => c.Id) + 1;
            characters.Add(item);
            serviceResponse.Data = characters.Select(c => mapper.Map<GetCharacterDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int Id)
        {
            ServiceResponse<List<GetCharacterDTO>> serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();

            try{
                Character character = characters.First(c => c.Id == Id);
                characters.Remove(character);
                serviceResponse.Data = characters.Select(c => mapper.Map<GetCharacterDTO>(c)).ToList();
                serviceResponse.Message = "Character Deleted Successfully";
            }catch(Exception e){
                serviceResponse.Status = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharcters()
        {
            return new ServiceResponse<List<GetCharacterDTO>>
            {
                Data = characters.Select(c => mapper.Map<GetCharacterDTO>(c)).ToList()
            };
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDTO>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = mapper.Map<GetCharacterDTO>(character);
            return serviceResponse;    
        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UdpateCharacterDTO character)
        {
            ServiceResponse<GetCharacterDTO> serviceResponse = new ServiceResponse<GetCharacterDTO>();

           try{ 
                Character? characterToUpdate = characters.FirstOrDefault(c => c.Id == character.Id);
                UpdateCharacterProperties(characterToUpdate, character);
                serviceResponse.Data = mapper.Map<GetCharacterDTO>(characterToUpdate);  
                serviceResponse.Message = "Character Updated Sucessfully";
            }catch(Exception e){
                serviceResponse.Status = false;
                serviceResponse.Message = e.Message;
            }


            return serviceResponse;
        }

        private void UpdateCharacterProperties(Character? characterToUpdate, UdpateCharacterDTO character)
        {
            characterToUpdate.Name = character.Name;
            characterToUpdate.HitPoints = character.HitPoints;
            characterToUpdate.Intellegence = character.Intellegence;
            characterToUpdate.Strenght = character.Strenght;
            characterToUpdate.Class = character.Class;
        }
    }
}