namespace Services
{
    public class MailTemplateService : Contracts.IMailTemplateService
    {
        private string GetBaseTemplate(string title, string content)
        {
            return $@"
                <html>
                <head>
                    <meta charset='utf-8'>
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; margin: 0; padding: 0; }}
                        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                        .header {{ background-color: #4CAF50; color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0; }}
                        .content {{ background-color: #f9f9f9; padding: 20px; border-radius: 0 0 5px 5px; }}
                        .info-row {{ margin: 10px 0; padding: 10px; background-color: white; border-left: 4px solid #4CAF50; }}
                        .info-box {{ margin: 20px 0; padding: 15px; background-color: #e8f5e9; border-radius: 5px; }}
                        .footer {{ text-align: center; margin-top: 20px; color: #666; font-size: 12px; padding: 20px; }}
                        .button {{ display: inline-block; padding: 12px 24px; background-color: #4CAF50; color: white; text-decoration: none; border-radius: 5px; margin: 10px 0; }}
                        .success-icon {{ font-size: 48px; text-align: center; margin: 20px 0; }}
                        .warning-box {{ background-color: #fff3cd; border-left: 4px solid #ffc107; padding: 15px; margin: 20px 0; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1>{title}</h1>
                        </div>
                        <div class='content'>
                            {content}
                        </div>
                        <div class='footer'>
                            <p>Bu otomatik bir bilgilendirme mesajıdır, lütfen yanıtlamayınız.</p>
                            <p>Sorularınız için müşteri hizmetlerimizle iletişime geçebilirsiniz.</p>
                        </div>
                    </div>
                </body>
                </html>
            ";
        }

        public string GetPasswordResetTemplate(string userName, string resetLink)
        {
            var content = $@"
                <div class='success-icon'>🔐</div>
                <p>Merhaba <strong>{userName}</strong>,</p>
                <p>Şifre sıfırlama talebinizi aldık.</p>
                
                <div class='warning-box'>
                    ⚠️ <strong>Dikkat:</strong> Bu talebi siz yapmadıysanız bu e-postayı dikkate almayınız.
                </div>
                
                <div style='text-align: center; margin: 30px 0;'>
                    <a href='{resetLink}' class='button'>Şifremi Sıfırla</a>
                </div>
                
                <div class='info-box'>
                    ⏰ Bu link 24 saat geçerlidir.<br/>
                    Güvenliğiniz için linki kimseyle paylaşmayınız.
                </div>
                
                <p style='margin-top: 20px; font-size: 12px; color: #666;'>
                    Butona tıklayamıyorsanız, aşağıdaki linki tarayıcınıza kopyalayınız:<br/>
                    <span style='word-break: break-all;'>{resetLink}</span>
                </p>
                
                <p style='margin-top: 20px;'>
                    Güvenli kalın,<br/>
                    <strong>Güvenlik Ekibi</strong>
                </p>
            ";

            return GetBaseTemplate("Şifre Sıfırlama Talebi", content);
        }
    }
}
