using SQLite;
using UnityEngine;

namespace DB.DBModels
{
    [Table("Player")]
    public class PlayerDBModel
    {

        [PrimaryKey]
        public string user_id { get; set; }
        
        public int score { get; set; }


    }
}
