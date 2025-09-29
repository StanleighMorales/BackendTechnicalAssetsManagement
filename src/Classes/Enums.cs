namespace BackendTechnicalAssetsManagement.src.Classes
{
    public class Enums
    {
        //Color code New = Green, Good = Teal/Blue, Defective = Red, NeedsRepair = Orange, Refurbished = Purple
        public enum ItemCondition { New, Good, Defective, Refurbished, NeedRepair }

        public enum ItemCategory { Electronics, Keys, MediaEquipment, Tools, Miscellaneous }

        public enum UserRole { Admin, Staff, Teacher, Student }

        public enum LentItemRemarks { Borrowed, Returned, Lost, Damaged, UnderRepair }

    }
}
    