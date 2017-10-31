using Wr.Classes;

namespace Gertec
{
    class Utilitarios
    {
        public static string getArquivoImportacao(WinRegistry Registro)
        {
            return Registro.getValue(Consts.TERMINALNOME, "Ultimo arquivo importado", "");
        }

        public static void setArquivoImportacao(WinRegistry Registro, string value)
        {
            Registro.setValue(Consts.TERMINALNOME, "Ultimo arquivo importado", value);
        }
    }
}
