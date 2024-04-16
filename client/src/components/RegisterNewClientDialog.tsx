import { Button } from "./ui/button";
import RegisterNewClientForm from "./RegisterNewClientForm";
import {
  Dialog,
  DialogContent,
  DialogTrigger,
  DialogHeader,
  DialogTitle,
} from "./ui/dialog";
import { createClient } from "@/services/api";
import { toast } from "./ui/use-toast";
import { useState } from "react";

export default function RegisterNewClientDialog() {
  const [open, setOpen] = useState(false);

  return (
    <Dialog open={open} onOpenChange={(value) => setOpen(value)} >
      <DialogTrigger>
        <Button>Cadastrar Novo Cliente</Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Cadastrar Novo Cliente</DialogTitle>
        </DialogHeader>
        <RegisterNewClientForm
          onSubmitClick={async (value) => {
            const newClient = await createClient(value);
            if (newClient) {
              toast({
                title: "Cliente cadastrado com sucesso",
              });
              setOpen(false);
            } else {
              toast({
                title: "Erro ao cadastrar cliente",
              });
            }
          }}
        />
      </DialogContent>
    </Dialog>
  );
}
