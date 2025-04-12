using DesafioBtg.Dominio.AutenticacoesDoisFatores.Repositorios.Interfaces;
using OtpNet;
using QRCoder;

namespace DesafioBtg.Infra.AutenticacoesDoisFatores.Repositorios;

public class AutenticacoesDoisFatores : IAutenticacoesDoisFatores
{
    public string GerarChaveSecreta()
    {
        byte[] chaveSecreta = KeyGeneration.GenerateRandomKey(20);

        return Base32Encoding.ToString(chaveSecreta);
    }

    public string GerarQrCodeUri(string email, string chaveSecreta)
    {
        return $"otpauth://totp/ClinicaDoPovo:{email}?secret={chaveSecreta}&issuer=ClinicaDoPovo";
    }

    public string GerarQrCode(string uri)
    {
        using var gerarQrCode = new QRCodeGenerator();

        using var dadosQrCode = gerarQrCode.CreateQrCode(uri, QRCodeGenerator.ECCLevel.Q);

        using var qrCode = new PngByteQRCode(dadosQrCode);

        byte[] imagemQrCode = qrCode.GetGraphic(20);

        return Convert.ToBase64String(imagemQrCode);
    }

    public bool ValidarCodigo(string chaveSecreta, string codigo)
    {
        var totp = new Totp(Base32Encoding.ToBytes(chaveSecreta));

        return totp.VerifyTotp(codigo, out _, new VerificationWindow(previous: 1, future: 1));

        // VerificationWindow acrescenta uma tolerância de 30 segundos para mais ou para menos.
    }
}
