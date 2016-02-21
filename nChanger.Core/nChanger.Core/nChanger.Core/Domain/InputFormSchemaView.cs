namespace nChanger.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InputFormSchemaView")]
    public partial class InputFormSchemaView
    {
        [StringLength(128)]
        public string TABLE_CATALOG { get; set; }

        [StringLength(128)]
        public string TABLE_SCHEMA { get; set; }

        [Key]
        public string TABLE_NAME { get; set; }

        [StringLength(128)]
        public string COLUMN_NAME { get; set; }

        public int? ORDINAL_POSITION { get; set; }

        [StringLength(4000)]
        public string COLUMN_DEFAULT { get; set; }

        [StringLength(3)]
        public string IS_NULLABLE { get; set; }

        [StringLength(128)]
        public string DATA_TYPE { get; set; }

        public int? CHARACTER_MAXIMUM_LENGTH { get; set; }

        public int? CHARACTER_OCTET_LENGTH { get; set; }

        public byte? NUMERIC_PRECISION { get; set; }

        public short? NUMERIC_PRECISION_RADIX { get; set; }

        public int? NUMERIC_SCALE { get; set; }

        public short? DATETIME_PRECISION { get; set; }

        [StringLength(128)]
        public string CHARACTER_SET_CATALOG { get; set; }

        [StringLength(128)]
        public string CHARACTER_SET_SCHEMA { get; set; }

        [StringLength(128)]
        public string CHARACTER_SET_NAME { get; set; }

        [StringLength(128)]
        public string COLLATION_CATALOG { get; set; }

        [StringLength(128)]
        public string COLLATION_SCHEMA { get; set; }

        [StringLength(128)]
        public string COLLATION_NAME { get; set; }

        [StringLength(128)]
        public string DOMAIN_CATALOG { get; set; }

        [StringLength(128)]
        public string DOMAIN_SCHEMA { get; set; }

        [StringLength(128)]
        public string DOMAIN_NAME { get; set; }
    }
}
