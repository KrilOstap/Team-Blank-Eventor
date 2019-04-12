using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IOrganizerService
    {
        IEnumerable<OrganizerDTO> GetOrganizers();
        OrganizerDTO GetById(int id);
        void Delete(int id);
        void Create(OrganizerDTO organizerDTO);      
    }
}
