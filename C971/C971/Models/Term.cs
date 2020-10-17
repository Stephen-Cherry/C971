using SQLite;
using System;
using System.Collections.Generic;

namespace C971.Models
{
    [Table("Terms")]
    public class Term
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int TermID { get; set; }
        public string TermTitle { get; set; }
        public DateTime TermStartDate { get; set; }
        public DateTime TermEndDate { get; set; }
    }
}