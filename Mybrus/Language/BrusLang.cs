using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mybrus.Language
{
    public static class BrusLang
    {
        private static bool _isEnglish = true;
        private static int _langId = 1;
        private static Notice _notice;
        public static int LangId { 
            get{
                return _langId;
            }
            set {
                _langId = value;
                _isEnglish = _langId == 1;
            }
        }
        public static string About { 
            get{
                return _isEnglish
                    ? "About"
                    : "Giới Thiệu"
                ;
            }
        }
        public static string BrushBrand
        {
            get
            {
                return _isEnglish
                    ? "Brush Brand"
                    : "Loại Cọ"
                ;
            }
        }
        public static string Cart_Add
        {
            get
            {
                return _isEnglish
                    ? "ADD TO CART"
                    : "Thêm Giỏ Hàng"
                ;
            }
        }
        public static string Contact
        {
            get
            {
                return _isEnglish
                    ? "Contact"
                    : "Liên Hệ"
                ;
            }
        }
        public static string Home
        {
            get
            {
                return _isEnglish
                    ? "Home"
                    : "Trang Chủ"
                ;
            }
        }
        public static string Language
        {
            get
            {
                return _isEnglish
                    ? "English"
                    : "Tiếng Việt"
                ;
            }
        }
        public static string Prod_Detail
        {
            get
            {
                return _isEnglish
                    ? "Detail"
                    : "Chi Tiết"
                ;
            }
        }
        public static string SignIn
        {
            get
            {
                return _isEnglish
                    ? "Sign In"
                    : "Đăng Nhập"
                ;
            }
        }
        public static string SiteName
        {
            get
            {
                return _isEnglish
                    ? "Mybrus"
                    : "Cọ Nail"
                ;
            }
        }
        public static string Slogan
        {
            get
            {
                return _isEnglish
                    ? "BEST BRUSHES YOU EVER SEEN"
                    : "CỌ NAIL TỐT NHẤT TRÊN THỊ TRƯỜNG"
                ;
            }
        }
        public static string Subscribe
        {
            get
            {
                return _isEnglish
                    ? "Subcribe nơ and get the latest offers"
                    : "Đăng ký ngay để có khuyến mãi tốt nhất"
                ;
            }
        }
        public static string Subscribe_Email
        {
            get
            {
                return _isEnglish
                    ? "Type your best Email Address"
                    : "Vui lòng nhập vào Email"
                ;
            }
        }

        public static Notice Notice { 
            get{
                _notice = new Notice(_langId);
                return _notice;
            }
        }
    }

    public class Notice {
        private bool _isEnglish = true;
        public Notice(int langId){
            _isEnglish = langId == 1;
        }
        public string CrimpingBrush
        {
            get
            {
                return _isEnglish
                    ? "Crimping Brush"
                    : "Bóp Cọ"
                ;
            }
        }
        public string Email
        {
            get
            {
                return _isEnglish
                    ? "Enter a valid email please!"
                    : "Nhập vào địa chỉ email nếu có!"
                ;
            }
        }
        public string Shipping_Address { 
            get{
                return _isEnglish
                    ? "Shipping"
                    : "Thông Tin Giao Hàng"
                ;
            }
        }
        public string ShippingAddress
        {
            get
            {
                return _isEnglish
                    ? "Tell me where to ship please!"
                    : "Vui lòng nhập vào địa chỉ giao hàng!"
                ;
            }
        }
        public string Shipping_Cost
        {
            get
            {
                return _isEnglish
                    ? "Shipping"
                    : "Phí giao hàng"
                ;
            }
        }
        public string ShippingCost
        {
            get
            {
                return _isEnglish
                    ? "Shipping is free if order is over $30!"
                    : "Giao hàng miễn phí nếu order hơn 30 dollar!"
                ;
            }
        }
    }
}