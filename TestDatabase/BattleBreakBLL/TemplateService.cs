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

        public void TemplateAddL(TemplateModel templateID, TemplateModel templateGame, TemplateModel templateName, TemplateModel templateMinimumPlayers, TemplateModel templateRules, TemplateModel templateWinCondition)
        {
            TemplateDAL templateDAL = new();
            TemplateDTO templateDTO = new();
            templateDTO.id = templateID.id;
            templateName.name = templateDTO.name;
            templateGame.game = templateDTO.game;
            templateDTO.minimumPlayers = templateMinimumPlayers.minimumPlayers;
            templateDTO.rules = templateRules.rules;
            templateDTO.winCondition = templateWinCondition.winCondition;
            templateDAL.TemplateAddD(templateDTO);
        }

        public void DeleteTemplateL(int templateID)
        {
            TemplateDAL templateDAL = new();
            templateDAL.DeleteTemplateD(templateID);
        }
    }

}
