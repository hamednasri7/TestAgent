using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models;

public class User
{
    private const string PersianLetters = "آابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهیيكءأإئؤة";
    public const string PersianNamePattern =
        "^[" + PersianLetters + "]+( [" + PersianLetters + "]+)*$";

    public int Id { get; set; }

    [Required(ErrorMessage = "نام الزامی است.")]
    [StringLength(50, ErrorMessage = "نام نباید بیشتر از 50 کاراکتر باشد.")]
    [RegularExpression(PersianNamePattern,
        ErrorMessage = "نام باید فارسی باشد، بدون فاصله ابتدا/انتها و بدون دو فاصله پشت سر هم.")]
    [Display(Name = "نام")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "نام خانوادگی الزامی است.")]
    [StringLength(50, ErrorMessage = "نام خانوادگی نباید بیشتر از 50 کاراکتر باشد.")]
    [RegularExpression(PersianNamePattern,
        ErrorMessage = "نام خانوادگی باید فارسی باشد، بدون فاصله ابتدا/انتها و بدون دو فاصله پشت سر هم.")]
    [Display(Name = "نام خانوادگی")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "سن الزامی است.")]
    [Range(1, 120, ErrorMessage = "سن باید عددی بین 1 تا 120 باشد.")]
    [Display(Name = "سن")]
    public int Age { get; set; }

    [Required(ErrorMessage = "موبایل الزامی است.")]
    [RegularExpression(@"^09\d{9}$", ErrorMessage = "موبایل باید 11 رقم بوده و با 09 شروع شود.")]
    [Display(Name = "موبایل")]
    public string Mobile { get; set; } = string.Empty;
}
