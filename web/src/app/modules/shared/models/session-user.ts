interface SessionUser {
  id: string;
  email: string;
  firstName: string;
  fullName: string;
  userName: string;
  isSysAdmin: boolean;
  type: string;
  acceptedTerms: boolean;
  identification: string;
  avatarUrl: string;
}


export {SessionUser};
