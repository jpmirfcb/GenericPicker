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
    public class ArepaDataAccess
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
                        x.Name.Contains(request.Search) || x.Ingredients.Contains(request.Search) ||
                        x.Region.Contains(request.Search)).ToList();
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


        private async Task<IEnumerable<ArepaItem>> GetAll()
        {
            return await Task.FromResult(new List<ArepaItem>()
            {
                new ArepaItem(){ Name= "Pelúa (Hairy)" , Ingredients = "Shredded meat and shredded gouda cheese", Region = "Caracas", ImageName = "arepa-pelua.jpg"},
                new ArepaItem(){ Name= "Reina Pepiada (Strong Queen)" , Ingredients = "Avocado, mayonnaise, shredded chicken breast", Region = "Caracas", ImageName = "reina-pepiada.jpg"},
                new ArepaItem(){ Name= "Viuda (Widow)" , Ingredients = "Nothing, she's lonely'", Region = "Maracaibo", ImageName="viuda.jpg"},
                new ArepaItem(){ Name= "Catira (Blondie)" , Ingredients = "Shredded chicken breast and gouda cheese", Region = "Caracas", ImageName = "catira.jpg"},
                new ArepaItem(){ Name= "Dominó (Domino)" , Ingredients = "Black beans and shredded white cheese", Region = "Caracas", ImageName="domino.jpg"},
                new ArepaItem(){ Name= "Gringa" , Ingredients = "Classical hamburger ingredients inside an arepa", Region = "Caracas", ImageName="gringa.jpg"},
                new ArepaItem(){ Name= "Llanera (Country)" , Ingredients = "Thin sliced meat, tomato sliced avocado and guayanes cheese", Region = "Caracas", ImageName = "llanera.jpg"},
                new ArepaItem(){ Name= "Pabellón" , Ingredients = "Black beans, shredded meat, fried sweet plantains and shredded white cheese", Region = "Caracas", ImageName="pabellon.jpg"},
                new ArepaItem(){ Name= "Pata-Pata" , Ingredients = "Black beans, shredded white cheese and sliced avocado", Region = "Caracas", ImageName = "patapata.jpg"},
                new ArepaItem(){ Name= "Pernil (Pork leg)" , Ingredients = "Roasted pork leg meat with sliced tomato and mayonnaise", Region = "Caracas", ImageName = "pernil.jpg"},
                new ArepaItem(){ Name= "Rumbera (Party Girl)" , Ingredients = "Roasted pork leg meat with shredded gouda cheese", Region = "Caracas", ImageName="rumbera.jpg"},
                new ArepaItem(){ Name= "Sifrina (Posh Girl)" , Ingredients = "Typical reina pepiada with gouda cheese", Region = "Caracas", ImageName = "sifrina.jpg"},
                new ArepaItem(){ Name= "Santa Bárbara (Saint Barbara)" , Ingredients = "Steak with white cheese and avocado", Region = "Caracas", ImageName="santa-barbara.jpg"},
                new ArepaItem(){ Name= "Rompe Colchón (Mattress Breaker)" , Ingredients = "Asorted seafood (octopus, calamar, oisters, shrimps) in vinaigrette sauce", Region = "Caracas", ImageName = "rompe-colchon.jpg"}
            });
        }
    }
}
