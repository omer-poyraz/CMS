using System.Text.Json;

namespace Entities.Models
{
    public class UserProfile
    {
        public int ID { get; set; }
        public string? Profile { get; set; }
        public string? Gender { get; set; }
        public string? Weight { get; set; }
        public string? TargetWeight { get; set; }
        public string? DiseaseInfo { get; set; }
        public string? MedicationInfo { get; set; }
        public string? NutritionHabits { get; set; }
        public bool? HasParentalApproval { get; set; }
        public bool? KVKKApproval { get; set; }
        public string? IdentityNumber { get; set; }
        public string? Occupation { get; set; }
        public string? ContactMethod { get; set; }
        public string? Reference { get; set; }
        public string? Height { get; set; }
        public int? MealsPerDay { get; set; }
        public JsonDocument? DislikedFoods { get; set; }
        public JsonDocument? AllergyFoodSelection { get; set; }
        public JsonDocument? HealthIssue { get; set; }
        public bool? UnderDoctorControl { get; set; }
        public bool? BloodThinner { get; set; }
        public string? RegularMedications { get; set; }
        public string? OperationHistory { get; set; }
        public string? Antidepressants { get; set; }
        public string? Vitamins { get; set; }
        public bool? Pregnancy { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? ProgramStartDate { get; set; }
        public string? UserId { get; set; }
        public JsonDocument? User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateddAt { get; set; }
    }
}