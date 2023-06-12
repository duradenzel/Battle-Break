using BattleBreakBLL.Models;
using BattleBreakDAL;
using BattleBreakDAL.DTOS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public void ChangeTemplates(int id, string game, string name, int minimumPlayers, string winCondition, string rules)
        {
            TemplateDTO template = new TemplateDTO();
            template.id = id;
            template.game = game;
            template.name = name;
            template.minimumPlayers = minimumPlayers;
            template.winCondition = winCondition;
            template.rules = rules;
            
            TemplateDAL templateDAL = new TemplateDAL();
            templateDAL.EditTemplate(template);
        }
    }

}
