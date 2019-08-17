using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskSchedule.Domain {
  public class TaskUser {

    public TaskUser () {
      this.DateOfCreate = System.DateTime.Now;
      this.DateOfEnd = null;
      this.FundationName = "NoFundation";
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

    [DataType (DataType.Date)]
    [DisplayFormat (DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [Display (Name = "Data da Finalização")]
    public Nullable<DateTime> DateOfEnd { get; set; }
    public bool Done { get; set; }

    public bool IsDeleted { get; set; }

    [Display (Name = "Controlado por Horário")]
    public bool IsHourControl { get; set; }

    [Display (Name = "Local da Prova")]
    public Nullable<int> LocationId { get; set; }

    public virtual ApplicationUser User { get; set; }
    public virtual Location Location { get; set; }

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

    public string GetUrlFromCalendar () {
      string url = "";

      url = "https://calendar.google.com/calendar/r/eventedit";
      url += "?text=" + this.FundationName + ".: (" + this.StudentId + "): " + this.StudentName;
      url += "&details=" + " <b>Instituição:</b> " + this.FundationName + "%0A" +
        " <b>ID do Estudante:</b> " + this.StudentId + "%0A" +
        " <b>Estudande:</b> " + this.StudentName + "%0A" +
        " <b>Responsavel:</b> " + this.User.Name + "%0A" +
        " <b>Fone:</b> " + this.Location.Phone + "%0A" +
        " <b>Endereco:</b> " + this.Location.FullAddress () + "%0A" +
        " <b>WebSite:</b> " + this.Link;
      url += "&location=" + this.Location.FullAddress ();
      url += "&dates=" + this.DateOfTest.ToString ("yyyyMMddThhmmss") + "/" + this.DateOfTest.AddMinutes (60).ToString ("yyyyMMddThhmmss");
      url += "&sprop=website:" + this.Link + "&sprop=name:" + this.FundationName;
      url += " & trp = true ";

      return url;
    }

  }
}
