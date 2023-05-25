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
                t.name = dto.name;
                t.minimumPlayers = dto.minimumPlayers;
                t.rules = dto.rules;
                t.winCondition = dto.winCondition;
                templateModel.Add(t);
            }
            return templateModel;
        }
        public void TemplateAddL(int templateID, string templateName, int templateMinimumPlayers, string templateRules, string templateWinCondition)
        {
            TemplateDAL templateDAL = new();
            templateDAL.TemplateAddD(templateID, templateName, templateMinimumPlayers, templateRules, templateWinCondition);
        }

        public void DeleteTemplateL(int templateID)
        {
            TemplateDAL templateDAL = new();
            templateDAL.DeleteTemplateD(templateID);
        }
    }

}
