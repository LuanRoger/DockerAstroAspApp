
import { Button } from "./ui/button";
import RegisterNewClientForm from "./RegisterNewClientForm";
import { Dialog, DialogContent, DialogTrigger, DialogHeader, DialogTitle } from "./ui/dialog";


export default function RegisterNewClientDialog() {
  return (
    <Dialog>
      <DialogTrigger>
        <Button>
          Cadastrar Novo Cliente
        </Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>
          Cadastrar Novo Cliente
          </DialogTitle>
        </DialogHeader>
        <RegisterNewClientForm submitButton={(
          <Button type="submit">
            Cadastrar
          </Button>
        )}/>
      </DialogContent>
    </Dialog>
  );
}
