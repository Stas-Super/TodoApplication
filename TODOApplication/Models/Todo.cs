using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TODOApplication.Models {
  /*id: 1,
    title: '...',
    body: '...',
    userId: 1*/
  //[Table("Task")]
  public class Todo {
    /// <summary>
    /// Task ID
    /// </summary>
    [NotNull]
    [PrimaryKey]
    [JsonProperty("id")]
    public int Id { get; set; }

    /// <summary>
    /// task name
    /// </summary>
    [NotNull]
    [JsonProperty("title")]
    public string Title { get; set; }
    /// <summary>
    /// User ID
    /// </summary>
    //[NotNull]
    //public int UserId { get; set; }

    public DateTime? Date { get; set; }

    public string FileName { get; set; }
    public byte[] FileContent { get; set; }
  }
}
