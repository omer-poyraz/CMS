namespace Entities.Response
{
    public static class Messages
    {
        public static class Success
        {
            // English Messages (EN)
            public const string CreatedEN = "Record created successfully";
            public const string UpdatedEN = "Record updated successfully";
            public const string DeletedEN = "Record deleted successfully";
            public const string RetrievedEN = "Record retrieved successfully";
            public const string ListedEN = "Records listed successfully";
            public const string PasswordChangedEN = "Password has been changed successfully";
            public const string PasswordResetEN = "Password reset email has been sent successfully";
            public const string LoginSuccessEN = "Login successful";
            public const string RegisterSuccessEN = "Registration completed successfully";
            public const string LogoutSuccessEN = "Logout successful";
            public const string TokenRefreshedEN = "Token refreshed successfully";

            // Turkish Messages (TR)
            public const string CreatedTR = "Kayıt başarıyla oluşturuldu";
            public const string UpdatedTR = "Kayıt başarıyla güncellendi";
            public const string DeletedTR = "Kayıt başarıyla silindi";
            public const string RetrievedTR = "Kayıt başarıyla getirildi";
            public const string ListedTR = "Kayıtlar başarıyla listelendi";
            public const string PasswordChangedTR = "Şifre başarıyla değiştirildi";
            public const string PasswordResetTR = "Şifre sıfırlama e-postası başarıyla gönderildi";
            public const string LoginSuccessTR = "Giriş başarılı";
            public const string RegisterSuccessTR = "Kayıt işlemi başarıyla tamamlandı";
            public const string LogoutSuccessTR = "Çıkış başarılı";
            public const string TokenRefreshedTR = "Token başarıyla yenilendi";

            // French Messages (FR)
            public const string CreatedFR = "Enregistrement créé avec succès";
            public const string UpdatedFR = "Enregistrement mis à jour avec succès";
            public const string DeletedFR = "Enregistrement supprimé avec succès";
            public const string RetrievedFR = "Enregistrement récupéré avec succès";
            public const string ListedFR = "Enregistrements listés avec succès";
            public const string PasswordChangedFR = "Le mot de passe a été modifié avec succès";
            public const string PasswordResetFR = "L'e-mail de réinitialisation du mot de passe a été envoyé avec succès";
            public const string LoginSuccessFR = "Connexion réussie";
            public const string RegisterSuccessFR = "Inscription terminée avec succès";
            public const string LogoutSuccessFR = "Déconnexion réussie";
            public const string TokenRefreshedFR = "Token actualisé avec succès";

            // Russian Messages (RU)
            public const string CreatedRU = "Запись успешно создана";
            public const string UpdatedRU = "Запись успешно обновлена";
            public const string DeletedRU = "Запись успешно удалена";
            public const string RetrievedRU = "Запись успешно получена";
            public const string ListedRU = "Записи успешно перечислены";
            public const string PasswordChangedRU = "Пароль успешно изменен";
            public const string PasswordResetRU = "Письмо для сброса пароля успешно отправлено";
            public const string LoginSuccessRU = "Вход выполнен успешно";
            public const string RegisterSuccessRU = "Регистрация успешно завершена";
            public const string LogoutSuccessRU = "Выход выполнен успешно";
            public const string TokenRefreshedRU = "Токен успешно обновлен";

            // German Messages (DE)
            public const string CreatedDE = "Datensatz erfolgreich erstellt";
            public const string UpdatedDE = "Datensatz erfolgreich aktualisiert";
            public const string DeletedDE = "Datensatz erfolgreich gelöscht";
            public const string RetrievedDE = "Datensatz erfolgreich abgerufen";
            public const string ListedDE = "Datensätze erfolgreich aufgelistet";
            public const string PasswordChangedDE = "Passwort wurde erfolgreich geändert";
            public const string PasswordResetDE = "E-Mail zum Zurücksetzen des Passworts wurde erfolgreich gesendet";
            public const string LoginSuccessDE = "Anmeldung erfolgreich";
            public const string RegisterSuccessDE = "Registrierung erfolgreich abgeschlossen";
            public const string LogoutSuccessDE = "Abmeldung erfolgreich";
            public const string TokenRefreshedDE = "Token erfolgreich aktualisiert";

            // Arabic Messages (AR)
            public const string CreatedAR = "تم إنشاء السجل بنجاح";
            public const string UpdatedAR = "تم تحديث السجل بنجاح";
            public const string DeletedAR = "تم حذف السجل بنجاح";
            public const string RetrievedAR = "تم استرجاع السجل بنجاح";
            public const string ListedAR = "تم سرد السجلات بنجاح";
            public const string PasswordChangedAR = "تم تغيير كلمة المرور بنجاح";
            public const string PasswordResetAR = "تم إرسال بريد إعادة تعيين كلمة المرور بنجاح";
            public const string LoginSuccessAR = "تم تسجيل الدخول بنجاح";
            public const string RegisterSuccessAR = "تم إكمال التسجيل بنجاح";
            public const string LogoutSuccessAR = "تم تسجيل الخروج بنجاح";
            public const string TokenRefreshedAR = "تم تحديث الرمز المميز بنجاح";
        }

        public static class Error
        {
            // English Messages (EN)
            public const string NotFoundEN = "Record not found";
            public const string AlreadyExistsEN = "Record already exists";
            public const string ValidationErrorEN = "Validation error";
            public const string UnauthorizedAccessEN = "Unauthorized access";
            public const string ServerErrorEN = "Internal server error";
            public const string PasswordChangeFailedEN = "Failed to change password. Please try again";
            public const string PasswordResetFailedEN = "Failed to reset password. Please try again";
            public const string OldPasswordIncorrectEN = "Current password is incorrect";
            public const string InvalidCredentialsEN = "Invalid username or password";
            public const string AccountLockedEN = "Account is locked. Please try again later";
            public const string AccountNotConfirmedEN = "Please confirm your email first";
            public const string RegisterFailedEN = "Registration failed";
            public const string TokenExpiredEN = "Token has expired";
            public const string InvalidTokenEN = "Invalid token";
            public const string UserNotFoundEN = "User not found";

            // Turkish Messages (TR)
            public const string NotFoundTR = "Kayıt bulunamadı";
            public const string AlreadyExistsTR = "Kayıt zaten mevcut";
            public const string ValidationErrorTR = "Doğrulama hatası";
            public const string UnauthorizedAccessTR = "Yetkisiz erişim";
            public const string ServerErrorTR = "Sunucu hatası";
            public const string PasswordChangeFailedTR = "Şifre değiştirme başarısız. Lütfen tekrar deneyin";
            public const string PasswordResetFailedTR = "Şifre sıfırlama başarısız. Lütfen tekrar deneyin";
            public const string OldPasswordIncorrectTR = "Mevcut şifre yanlış";
            public const string InvalidCredentialsTR = "Geçersiz kullanıcı adı veya şifre";
            public const string AccountLockedTR = "Hesap kilitlendi. Lütfen daha sonra tekrar deneyin";
            public const string AccountNotConfirmedTR = "Lütfen önce e-postanızı onaylayın";
            public const string RegisterFailedTR = "Kayıt başarısız";
            public const string TokenExpiredTR = "Token süresi doldu";
            public const string InvalidTokenTR = "Geçersiz token";
            public const string UserNotFoundTR = "Kullanıcı bulunamadı";

            // French Messages (FR)
            public const string NotFoundFR = "Enregistrement non trouvé";
            public const string AlreadyExistsFR = "L'enregistrement existe déjà";
            public const string ValidationErrorFR = "Erreur de validation";
            public const string UnauthorizedAccessFR = "Accès non autorisé";
            public const string ServerErrorFR = "Erreur interne du serveur";
            public const string PasswordChangeFailedFR = "Échec du changement de mot de passe. Veuillez réessayer";
            public const string PasswordResetFailedFR = "Échec de la réinitialisation du mot de passe. Veuillez réessayer";
            public const string OldPasswordIncorrectFR = "Le mot de passe actuel est incorrect";
            public const string InvalidCredentialsFR = "Nom d'utilisateur ou mot de passe invalide";
            public const string AccountLockedFR = "Le compte est verrouillé. Veuillez réessayer plus tard";
            public const string AccountNotConfirmedFR = "Veuillez d'abord confirmer votre e-mail";
            public const string RegisterFailedFR = "Échec de l'inscription";
            public const string TokenExpiredFR = "Le token a expiré";
            public const string InvalidTokenFR = "Token invalide";
            public const string UserNotFoundFR = "Utilisateur non trouvé";

            // Russian Messages (RU)
            public const string NotFoundRU = "Запись не найдена";
            public const string AlreadyExistsRU = "Запись уже существует";
            public const string ValidationErrorRU = "Ошибка проверки";
            public const string UnauthorizedAccessRU = "Несанкционированный доступ";
            public const string ServerErrorRU = "Внутренняя ошибка сервера";
            public const string PasswordChangeFailedRU = "Не удалось изменить пароль. Пожалуйста, попробуйте снова";
            public const string PasswordResetFailedRU = "Не удалось сбросить пароль. Пожалуйста, попробуйте снова";
            public const string OldPasswordIncorrectRU = "Текущий пароль неверен";
            public const string InvalidCredentialsRU = "Неверное имя пользователя или пароль";
            public const string AccountLockedRU = "Аккаунт заблокирован. Пожалуйста, попробуйте позже";
            public const string AccountNotConfirmedRU = "Пожалуйста, сначала подтвердите свою электронную почту";
            public const string RegisterFailedRU = "Ошибка регистрации";
            public const string TokenExpiredRU = "Срок действия токена истек";
            public const string InvalidTokenRU = "Недействительный токен";
            public const string UserNotFoundRU = "Пользователь не найден";

            // German Messages (DE)
            public const string NotFoundDE = "Datensatz nicht gefunden";
            public const string AlreadyExistsDE = "Datensatz existiert bereits";
            public const string ValidationErrorDE = "Validierungsfehler";
            public const string UnauthorizedAccessDE = "Unbefugter Zugriff";
            public const string ServerErrorDE = "Interner Serverfehler";
            public const string PasswordChangeFailedDE = "Passwortänderung fehlgeschlagen. Bitte versuchen Sie es erneut";
            public const string PasswordResetFailedDE = "Passwortzurücksetzung fehlgeschlagen. Bitte versuchen Sie es erneut";
            public const string OldPasswordIncorrectDE = "Aktuelles Passwort ist falsch";
            public const string InvalidCredentialsDE = "Ungültiger Benutzername oder Passwort";
            public const string AccountLockedDE = "Konto ist gesperrt. Bitte versuchen Sie es später erneut";
            public const string AccountNotConfirmedDE = "Bitte bestätigen Sie zuerst Ihre E-Mail";
            public const string RegisterFailedDE = "Registrierung fehlgeschlagen";
            public const string TokenExpiredDE = "Token ist abgelaufen";
            public const string InvalidTokenDE = "Ungültiger Token";
            public const string UserNotFoundDE = "Benutzer nicht gefunden";

            // Arabic Messages (AR)
            public const string NotFoundAR = "السجل غير موجود";
            public const string AlreadyExistsAR = "السجل موجود بالفعل";
            public const string ValidationErrorAR = "خطأ في التحقق";
            public const string UnauthorizedAccessAR = "وصول غير مصرح به";
            public const string ServerErrorAR = "خطأ في الخادم الداخلي";
            public const string PasswordChangeFailedAR = "فشل تغيير كلمة المرور. الرجاء المحاولة مرة أخرى";
            public const string PasswordResetFailedAR = "فشل إعادة تعيين كلمة المرور. الرجاء المحاولة مرة أخرى";
            public const string OldPasswordIncorrectAR = "كلمة المرور الحالية غير صحيحة";
            public const string InvalidCredentialsAR = "اسم المستخدم أو كلمة المرور غير صالحة";
            public const string AccountLockedAR = "الحساب مقفل. الرجاء المحاولة لاحقاً";
            public const string AccountNotConfirmedAR = "الرجاء تأكيد بريدك الإلكتروني أولاً";
            public const string RegisterFailedAR = "فشل التسجيل";
            public const string TokenExpiredAR = "انتهت صلاحية الرمز";
            public const string InvalidTokenAR = "رمز غير صالح";
            public const string UserNotFoundAR = "المستخدم غير موجود";
        }
    }
}
