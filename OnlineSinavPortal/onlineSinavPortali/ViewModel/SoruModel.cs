using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace onlineSinavPortali.ViewModel
{
    public class SoruModel
    {
        public int soruId { get; set; }
        public string soruMetin { get; set; }
        public int? soruSinavId { get; set; }

        public List<SecenekModel> secenekModels;




}
}