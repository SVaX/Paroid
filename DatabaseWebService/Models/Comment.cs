using System;
using System.Collections.Generic;

namespace DatabaseWebService;

public partial class Comment
{
    public int CommentId { get; set; }

    public int IdUser { get; set; }

    public int IdApp { get; set; }

    public DateTime Date { get; set; }

    public string CommentText { get; set; } = null!;

    public int Score { get; set; }

    public bool IsReported { get; set; }

    public virtual Application IdAppNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
