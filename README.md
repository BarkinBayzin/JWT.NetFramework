# Progress

.NET Web API projesi oluşturacağız ve System.IdentityModel.Tokens.Jwt kütüphanesi kullanarak JWT token doğrulama işlemlerini gerçekleştireceğiz.

# Proje Oluşturma

Visual Studio açın ve File -> New -> Project adımlarını takip ederek yeni bir .NET Web API projesi oluşturun.

Projeyi oluşturduktan sonra, NuGet Paket Yöneticisi'nden "System.IdentityModel.Tokens.Jwt" paketini yükleyin.

# JWT Token Doğrulama Sınıfı

JWT token doğrulama işlemlerini gerçekleştirecek olan sınıfımızı oluşturacağız. Bunun için, Helpers klasörü altında JwtAuthentication.cs adında bir sınıf oluşturun.

Bu sınıf, JWT token doğrulama işlemlerini gerçekleştirmek için TryValidateToken() ve GenerateToken() methodlarını içerir.

TryValidateToken() methodu, bir JWT token'ın doğruluğunu kontrol eder ve doğruysa, ClaimsPrincipal nesnesi döndürür. Bu method, TokenValidationParameters sınıfı kullanılarak JWT token'ın doğrulanması işlemini gerçekleştirir.

GenerateToken() methodu, bir ClaimsIdentity nesnesi ve token'ın geçerlilik süresi (expiration time) parametreleri alır ve bir JWT token oluşturur.

# JWT Token Doğrulama Attribute'ü

JWT token doğrulama işlemini bir attribute haline getireceğiz. Bunun için, Attributes klasörü altında JwtAuthenticationAttribute.cs adında bir sınıf oluşturun.

Bu sınıf, AuthorizationFilterAttribute sınıfından kalıtımalır ve OnAuthorization() methodunu override ederek JWT token doğrulama işlemini gerçekleştirir.

OnAuthorization() methodu, gelen isteği kontrol eder ve JWT token'ın varlığını ve doğruluğunu doğrular. Eğer token geçerli değilse, isteği reddeder ve HttpStatusCode.Unauthorized cevabı verir.

# JWT Token Doğrulama Attribute'ü Kullanımı

JWT token doğrulama işlemini gerçekleştirecek olan attribute'ü, controller methodlarında kullanabiliriz. Bunun için, örneğin ValuesController sınıfını açın ve bir method'a attribute ekleyin:

JwtAuthentication attribute'ünü Get() methoduna ekledik. Bu sayede, Get() methodu sadece geçerli bir JWT token ile çağrılabilir.
