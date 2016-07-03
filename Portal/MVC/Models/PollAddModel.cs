using Data;
using Data.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class PollAddModel
    {
        [Required(ErrorMessage = "Morate uneti temu glasanja.")]
        [Display(Name = "Tema")]
        public string Topic { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Broj odgovora")]
        public bool AllowMultiple { get; set; }

        [Display(Name = "Sakrij rezultate")]
        public bool HideResultsUntilFinished { get; set; }

        [Display(Name = "Sakrij glasace")]
        public bool HideVoters { get; set; }

        [Required(ErrorMessage = "Morate uneti datum završetka glasanja.")]
        [Display(Name = "Kraj glasanja")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Morate uneti vreme završetka glasanja.")]
        public string EndTime { get; set; }

        [Display(Name = "Opcije")]
        public List<string> Options { get; set; }

        public PollAddModel()
        {
            EndDate = DateTime.Now.AddDays(3);
        }

        public static PollAddModel GetEditModel(int id)
        {
            PollDTO pollDTO = PollModel.CreatePollDTO(id);
            List<PollOptionDTO> pollOptionDTOs = PollModel.CreatePollOptionDTOs(id);

            return new PollAddModel
            {
                Topic = pollDTO.Topic,
                Description = pollDTO.Description,
                AllowMultiple = pollDTO.AllowMultiple,
                HideResultsUntilFinished = pollDTO.HideResultsUntilFinished,
                HideVoters = pollDTO.HideVoters,
                EndDate = pollDTO.EndDate,
                Options = pollOptionDTOs.Select(x => x.Answer).ToList()
            };
        }
    }
}