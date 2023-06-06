using BattleBreakBLL.Models;
using BattleBreakDAL;
using BattleBreakDAL.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleBreakBLL
{
    public class TemplateService
    {
        public List<TemplateModel> GetTemplates()
        {
            TemplateDAL templateDAL = new();
            List<TemplateModel> templateModel = new();
            List<TemplateDTO> templateDTO = templateDAL.GetTemplates();

            foreach(TemplateDTO dto in templateDTO)
            {
                TemplateModel t = new();
                t.id = dto.id;
                t.game = dto.game;
                t.name = dto.name;
                t.minimumPlayers = dto.minimumPlayers;
                t.rules = dto.rules;
                t.winCondition = dto.winCondition;
                templateModel.Add(t);
            }
            return templateModel;
        }

        public List<string> GetGames()
        {
            TemplateDAL templateDAL = new TemplateDAL();
            List<string> gameNames = templateDAL.GetGames();
            return gameNames;
        }

        public void TemplateAddL(TemplateModel templateModel)
        {
            TemplateDAL templateDAL = new();
            TemplateDTO templateDTO = new();
            templateDTO.id = templateModel.id;
            templateDTO.name = templateModel.name;
            templateDTO.game = templateModel.game;
            templateDTO.minimumPlayers = templateModel.minimumPlayers;
            templateDTO.rules = templateModel.rules;
            templateDTO.winCondition = templateModel.winCondition;
            templateDAL.TemplateAddD(templateDTO);
        }

        public void DeleteTemplateL(int templateID)
        {
            TemplateDAL templateDAL = new();
            templateDAL.DeleteTemplateD(templateID);
        }
    }

}
