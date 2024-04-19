import axios from "axios";
import type Client from "./models/client";
import type { CreateClient } from "./models/create_client";
import type { UpdateClient } from "./models/update_client";

const baseUrl = import.meta.env.PUBLIC_API_ENDPOINT;
const clientsEndpoint = `${baseUrl}/client`;

export async function getUsers(page: number): Promise<Client[]> {
  const response = await axios.get<Client[]>(clientsEndpoint, {
    params: {
      page: page,
      pageSize: 10,
    },
  });
  if (response.status !== 200) {
    return [];
  }

  return response.data;
}

export async function updateClient(clientId: number, newClientInfo: UpdateClient): Promise<Client | null> {
  const response = await axios.put(`${clientsEndpoint}/${clientId}`, newClientInfo);

  if(response.status !== 200) {
    return null;
  }

  return response.data;
}

export async function deleteClient(clientId: number): Promise<boolean> {
  const response = await axios.delete(`${clientsEndpoint}/${clientId}`);

  return response.status === 200 && response.data === clientId;
}

export async function createClient(client: CreateClient): Promise<Client | null> {
  const response = await axios.post(clientsEndpoint, client);

  if(response.status !== 201 && response.status !== 200) {
    return null;
  }

  return response.data;
}