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
import { useStore } from "@nanostores/react";
import usersStore from "../stores/users_store";
import type { RegisterClient } from "@/lib/interfaces/register_client";
import type Client from "@/services/models/client";

export default function RegisterNewClientDialog() {
  const $users = useStore(usersStore);
  const [open, setOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  async function registerNewUser(clientInfo: RegisterClient) {
    setIsLoading(true);

    const newClient = await createClient(clientInfo);

    if (!newClient) {
      toast({
        title: "Erro ao cadastrar cliente",
      });
      return;
    }

    const client = {
      id: newClient.id,
      name: newClient.name,
      email: newClient.email,
    } satisfies Client;
    usersStore.set([...$users, client]);

    toast({
      title: "Cliente cadastrado com sucesso",
    });
    setOpen(false);
    setIsLoading(false);
  }

  return (
    <Dialog open={open} onOpenChange={(value) => setOpen(value)}>
      <DialogTrigger>
        <Button>Cadastrar Novo Cliente</Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Cadastrar Novo Cliente</DialogTitle>
        </DialogHeader>
        <RegisterNewClientForm
          isLoadingButton={isLoading}
          onSubmitClick={registerNewUser}
        />
      </DialogContent>
    </Dialog>
  );
}
