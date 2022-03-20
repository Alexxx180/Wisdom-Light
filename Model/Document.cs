using WisdomLight.Customing;
using System.Collections.Generic;

namespace WisdomLight.Model
{
    /// <summary>
    /// Serializable class to save user work
    /// </summary>
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