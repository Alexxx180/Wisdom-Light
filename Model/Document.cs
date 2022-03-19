using WisdomLight.Customing;
using System.Collections.Generic;

namespace WisdomLight.Model
{
    public class Document
    {
        public Document()
        {
            FileLocations = new List<string>();
            Information = new List<Expression>();
        }

        public void Refresh(Document filledDocument)
        {
            Information.Refresh(filledDocument.Information);
            FileLocations.Refresh(filledDocument.FileLocations);
        }

        public List<string> FileLocations { get; set; }
        public List<Expression> Information { get; set; }
    }
}