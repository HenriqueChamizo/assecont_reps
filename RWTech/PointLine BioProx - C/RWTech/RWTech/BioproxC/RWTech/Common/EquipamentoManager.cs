using BioproxC.RWTech.Pojo;

namespace BioproxC.RWTech.Common
{
    public class EquipamentoManager
    {
        private static EquipamentoManager instance;
        private Equipamento equipamento;

        public static EquipamentoManager getInstance()
        {
            if (instance == null)
            {
                instance = new EquipamentoManager();
            }
            return instance;
        }

        public Equipamento getEquipamento()
        {
            if (equipamento == null)
            {
                equipamento = new Equipamento();
            }
            return equipamento;
        }

        public void setEquipamento(Equipamento relogio)
        {
            this.equipamento = relogio;
        }
    }
}
