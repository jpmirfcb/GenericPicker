using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arepa.Sample.Infrastructure;
using Arepa.Sample.Infrastructure.POCO;

namespace Arepa.Sample.DataAccess
{
    public class FootballTeamDataAccess
    {
        public async Task<DataResponse<IEnumerable>> GetList(IPickerRequest request)
        {

            //TODO: Implement your own data access code here
            try
            {
                var response = await GetAll();
                var list = response.ToList();
                if (!string.IsNullOrEmpty(request.Search))
                {
                    list = list.Where(x =>
                        x.Name.Contains(request.Search) || x.StadiumName.Contains(request.Search) ||
                        x.Location.Contains(request.Search)).ToList();
                }

                var result = list.Skip(request.Offset).Take(request.ChunkSize);
                return new DataResponse<IEnumerable>()
                {
                    Results = result,
                    Message = string.Empty,
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new DataResponse<IEnumerable>()
                {
                    Results = null,
                    Success = false,
                    Message = e.Message
                };
            }
        }


        private async Task<IEnumerable<FootballTeamItem>> GetAll()
        {
            return await Task.FromResult(new List<FootballTeamItem>()
            {
                new FootballTeamItem(){ Name="FC Barcelona", StadiumName = "Camp Nou", Location = "Barcelona, Spain", ImageName = "fcbarcelona.png"},
                new FootballTeamItem(){ Name="Manchester United", StadiumName = "Old Trafford", Location = "Manchester, UK", ImageName = "manchester.png"},
                new FootballTeamItem(){ Name="Atletico de Madrid", StadiumName = "Wanda Metropolitano", Location = "Madrid, Spain", ImageName = "atleti.png"},
                new FootballTeamItem(){ Name="Manchester City", StadiumName = "Etihad Stadium", Location = "Manchester, UK", ImageName = "city.png"},
                new FootballTeamItem(){ Name="Real Madrid CF", StadiumName = "Santiago Bernabeu", Location = "Madrid, Spain", ImageName = "realdemadrid.png"},
                new FootballTeamItem(){ Name="Paris Saint-Germain", StadiumName = "Parc des Princes", Location = "Paris, France", ImageName = "psg.jpg"},
                new FootballTeamItem(){ Name="Bayern Munich", StadiumName = "Alianz Arena", Location = "Munich, Germany", ImageName = "bayern.png"},
                new FootballTeamItem(){ Name="Borussia Dortmund", StadiumName = "Signal Iduna Park", Location = "Dortmund, Germany", ImageName = "borussia.png"},
                new FootballTeamItem(){ Name="Internazionalle Milano", StadiumName = "San Siro", Location = "Milan, Italy", ImageName = "inter.png"},
                new FootballTeamItem(){ Name="AC Milan", StadiumName = "San Siro", Location = "Milan, Italy", ImageName = "milan.jpg"},
                new FootballTeamItem(){ Name="Sevilla FC", StadiumName = "Estadio Ramón Sanchez-Pizjuan", Location = "Sevilla, Spain", ImageName = "sevilla.jpg"},
                new FootballTeamItem(){ Name="Tottenham Hotspurs FC", StadiumName = "Tottenham Hotspurs Stadium", Location = "Tottenham, UK", ImageName = "tottenham.jpg"},
                new FootballTeamItem(){ Name="Valencia CF", StadiumName = "Estadio de Mestalla", Location = "Valencia, Spain", ImageName = "valencia.png"}

            });
        }

    }
}
