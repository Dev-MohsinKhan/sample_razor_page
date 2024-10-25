using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static SampleRazorPage.Models.PocoModel;

namespace SampleRazorPage.Pages
{
    public class RecordModel : PageModel
    {
        private readonly ApplicationDbContext _context;


        public RecordModel(ApplicationDbContext context)
        {
            _context = context;
            //Regions = new List<Region>();
        }



        //public Item items { get; set; }
        [BindProperty]
        public Record Record { get; set; }
        public List<Region> Regions { get; set; }
        public List<Wiliyat> Wiliyats { get; set; }
        public List<Area> Areas { get; set; }
        public List<Village> Villages { get; set; }
        public List<EntityType> EntityTypes { get; set; }
        public string ValidationErrors { get; set; }
        public string SuccessMessage { get; set; }
        public async Task OnGetAsync()
        {


            LoadData();
        }

        private void LoadData()
        {
            Record = new Record();
            EntityTypes = _context.EntityType.ToList();
            Regions = _context.Regions.ToList();
        }
        public async Task<JsonResult> OnGetWiliyatsByRegion(int regionId)
        {
            var wiliyats = await _context.Wiliyats
                .Where(w => w.RegionId == regionId)
                .ToListAsync();
            return new JsonResult(wiliyats);
        }

        public async Task<JsonResult> OnGetAreasByWiliyat(int wiliyatId)
        {
            var areas = await _context.Areas
                .Where(a => a.WiliyatId == wiliyatId)
                .ToListAsync();
            return new JsonResult(areas);
        }

        public async Task<JsonResult> OnGetVillagesByArea(int areaId)
        {
            var villages = await _context.Villages
                .Where(v => v.AreaId == areaId)
                .ToListAsync();
            return new JsonResult(villages);
        }
        public IActionResult OnPost()
        {

            ModelState.Remove("Record.Name");

            if (!ModelState.IsValid)
            {

                LoadData();
                ModelState.Remove("Record.Name");

                ValidationErrors = string.Join("<br />", ModelState.Values
             .SelectMany(v => v.Errors)
             .Select(e => e.ErrorMessage));

                return Page();
            }
            var VillageName = _context.Villages.Where(a => a.Id == Record.Village).Select(a => a.Name).FirstOrDefault();
            var AreaName = _context.Areas.Where(a => a.Id == Record.Area).Select(a => a.Name).FirstOrDefault();
            var WillayaName = _context.Wiliyats.Where(a => a.Id == Record.Willaya).Select(a => a.Name).FirstOrDefault();
            var RegionName = _context.Regions.Where(a => a.Id == Record.Region).Select(a => a.Name).FirstOrDefault();
            var EntityTypeName = _context.EntityType.Where(a => a.Id == Record.EntityType).Select(a => a.Name).FirstOrDefault();

            var newItem = new Item
            {
                Name = Record.Name ?? "",
                EntityType = Record.EntityType,

                EntityTypeName = EntityTypeName ?? "",
                Email = Record.Email,
                PhoneNumber = Record.PhoneNumber,
                Region = Record.Region,

                RegionName = RegionName ?? "",
                Willaya = Record.Willaya,

                WillayaName = WillayaName ?? "",
                Area = Record.Area,
                AreaName = AreaName ?? "",
                Village = Record.Village,

                VillageName = VillageName ?? "",
                DetailsODE = Record.DetailsODE,
                Remarks = Record.Remarks
            };

            _context.Item.Add(newItem); 

            // Save changes to the database
            _context.SaveChanges();
            SuccessMessage = "Record saved Successfully";
            LoadData();
            return Page();
            // Redirect to another page or display success message
            //return RedirectToPage("Record");
        }
    }
}
