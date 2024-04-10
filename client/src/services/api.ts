import axios from "axios";
import type Client from "./models/client";

const baseUrl = import.meta.env.PUBLIC_API_ENDPOINT;
const usersEndpoint = `${baseUrl}/user`;

export async function getUsers(page: number): Promise<Client[]> {
  const response = await axios.get<Client[]>(usersEndpoint, {
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

export async function deleteClient(clientId: number): Promise<boolean> {
  const response = await axios.delete(`${usersEndpoint}/${clientId}`);

  return response.status === 200 && response.data === clientId;
}
