using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskSchedule.Domain {
  public class TaskUser {

    public TaskUser () {
      this.DateOfCreate = System.DateTime.Now;
      this.DateOfEnd = null;
    }
    public int Id { get; set; }

    [Required]
    [DataType (DataType.DateTime)]
    [Display (Name = "Data da Criação")]
    public DateTime DateOfCreate { get; set; }

    [Required]
    [DataType (DataType.Date)]
    [DisplayFormat (DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [Display (Name = "Data da Tarefa")]
    public DateTime DateOfTest { get; set; }

    [Required]
    [DataType (DataType.Time)]
    [Display (Name = "Hora da Tarefa")]
    public TimeSpan Time { get; set; }

    [DataType (DataType.Time)]
    public TimeSpan? Duration { get; set; }

    [Required]
    [DataType (DataType.Text)]
    [Display (Name = "Instituição")]
    public string FundationName { get; set; }

    [Required]
    [DataType (DataType.Text)]
    [Display (Name = "Nome do Aluno")]
    public string StudentName { get; set; }

    [Required]
    [DataType (DataType.Text)]
    [Display (Name = "Identificação do Aluno")]
    public string StudentId { get; set; }

    [Required]
    [DataType (DataType.Text)]
    [Display (Name = "Supervisor")]
    public int UserId { get; set; }

    [Required]
    [DataType (DataType.Url)]
    [Display (Name = "Url do Teste")]
    public string Link { get; set; }

    [DataType (DataType.Text)]
    [Display (Name = "Obs")]
    public string Comments { get; set; }

    public Nullable<DateTime> DateOfEnd { get; set; }
    public bool Done { get; set; }

    public virtual ApplicationUser User { get; set; }

    public static List<string> TimeSpansInRange (TimeSpan start, TimeSpan end, TimeSpan interval) {

      List<string> timeSpans = new List<string> ();
      string time = "";
      while (start.Add (interval) <= end) {
        time = start.Add (interval).ToString (@"hh\:mm");
        timeSpans.Add (time);
        start = start.Add (interval);
      }
      return timeSpans;
    }

  }
}
