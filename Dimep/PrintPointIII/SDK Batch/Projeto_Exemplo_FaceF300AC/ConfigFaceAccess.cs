using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ExemploFaceF300AC
{
    class ConfigFaceAccess
    {
        public enum TipoIdioma
        {
            Ingles = 0,
            Portugues = 1,
        }
        public enum TipoSensor
        {
            Nenhum = 0,
            NormalmenteAberto = 1,
            NormalmenteFechado = 2
        }
        public enum TipoCartao
        {
            Wiegand26 = 0,
            Wiegand34 = 1
        }
        public enum TipoModoVerificacao
        {
            FaceOUCardOUPin = 9,
            Face = 10,
            FaceECard = 11,
            FaceEPin = 12,
            FaceECardEPin = 13,
        }
        public enum TipoModoCFoto
        {
            Nenhum = 0,
            UserPhoto = 1,
            RealTimeCamera = 2,
        }
        [CategoryAttribute("Autenticação"),
        DescriptionAttribute("Configura o modo de verificação quando não esta conectado ao aplicativo (em batch).")]
        public TipoModoVerificacao ModoVerificacaoBatch { get; set; }
        [CategoryAttribute("Basico"),
        DescriptionAttribute("Configura de idioma.")]
        public TipoIdioma Idioma { get; set; }
        [CategoryAttribute("Basico"),
        DescriptionAttribute("Configura de volume do audio, permitindo valores entre 0 e 10.")]
        public int Volume { get; set; }
        [CategoryAttribute("Autenticação"),
        DescriptionAttribute("Configura o tempo para permitir a verificação novamente, configura 0 para sempre verificar e maior que zero para valores até 255 em minutos.")]
        public int TempoReverificacao { get; set; }
        [CategoryAttribute("Segurança"),
        DescriptionAttribute("Informe uma senha numerica de até 20 caracteres, para PIN Master.")]
        public string PINMaster { get; set; }
        [CategoryAttribute("Energia"),
        DescriptionAttribute("Informe um valor para o periodo de hibernação em minutos.")]
        public int TempoHibernacao { get; set; }
        [CategoryAttribute("Logs"),
        DescriptionAttribute("Informe um valor maximo para alarme de logs de verificação.")]
        public int AlertaLogVerificacao { get; set; }
        [CategoryAttribute("Logs"),
        DescriptionAttribute("Informe um valor maximo para alarme de logs de sistema.")]
        public int AlertaLogSistema { get; set; }

        [CategoryAttribute("Sensor de Porta"),
        DescriptionAttribute("Informe um valor em segundos para o tempo de desbloqueio.")]
        public int TempoDesbloqueio { get; set; }
        [CategoryAttribute("Sensor de Porta"),
        DescriptionAttribute("Informe um valor em segundos para o limite de tempo de porta aberta.")]
        public int TempoLimitePortaAberta { get; set; }
        [CategoryAttribute("Sensor de Porta"),
        DescriptionAttribute("Informe o tipo do comportamento do sensor de porta.")]
        public TipoSensor TipoSensorPorta { get; set; }
        [CategoryAttribute("Outros"),
        DescriptionAttribute("Informe se o equipamento deve assistir interferências.")]
        public bool  AssistirInterferencia { get; set; }
        [CategoryAttribute("Outros"),
        DescriptionAttribute("Informe o tipo de leitura de cartão IN/OUT.")]
        public TipoCartao TipoLeitCartao { get; set; }
        [CategoryAttribute("Outros"),
        DescriptionAttribute("Ligar ou desligar os Leds de iluminação da camera.")]
        public bool  LigarLedsCamera { get; set; }
        [CategoryAttribute("Outros"),
        DescriptionAttribute("Informe se modo de captura de fotos.")]
        public TipoModoCFoto ModoCapturaFoto { get; set; }

    }
}
